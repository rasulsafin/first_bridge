using System;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using DM.Domain;
using DM.Domain.Implementations;
using DM.Domain.Interfaces;
using DM.Domain.Helpers;

using DM.DAL;

using Dotmim.Sync.PostgreSql;
using Dotmim.Sync;

namespace DM
{
    public class Startup
    {
        //TODO: Add logger
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IFieldService, FieldService>();
            services.AddScoped<IListFieldService, FieldService>();
            services.AddScoped<CurrentUserService>();

            services.AddDbContext<DmDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Db"), builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            });

            services.AddControllers();

            services.AddDistributedMemoryCache();

            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = "Dotmim.Sync.Bearer",
                            ValidAudience = "Dotmim.Sync.Bearer",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SOME_RANDOM_KEY_DO_NOT_SHARE"))
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                Debug.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                Debug.WriteLine("OnTokenValidated: " + context.SecurityToken);
                                return Task.CompletedTask;
                            }
                        };
                    });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DM", Version = "v1" });

                // Add jwt bearer token as a header for web api tagged by authorize
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                    "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            var connectionString = Configuration.GetSection("ConnectionStrings")["Db"];

            var tables = new SyncSetup(new string[] { "Comments", "Field", "Items", "List", "ListField",
                                                      "Organization", "Permissions", "Projects", "RecordField", "RecordList",
                                                      "Records", "Role", "Template", "TemplateField", "TemplateList", "UserProjectEntity", "Users"});

            services.AddSyncServer<NpgsqlSyncProvider>(connectionString, tables);

            //services.AddLocalization(options => options.ResourcesPath = "translations-folder (not exists yet)");

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors(options =>
            {
                options.AddPolicy("cors", policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "http://dm-dev.briogroup.ru", "http://dm-dev.briogroup.ru:443");
                    policy.WithMethods("GET", "POST", "PUT", "DELETE");
                    policy.AllowAnyHeader();
                    policy.AllowCredentials();
                });
            });

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DM v1"));
            }
            app.UseHttpsRedirection();
            app.UseCors("cors");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
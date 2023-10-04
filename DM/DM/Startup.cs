using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Serilog;

using DM.Domain.Interfaces;
using DM.Domain.Helpers;
using DM.Domain.Infrastructure;
using DM.Domain.Services;

using DM.DAL;
using DM.DAL.Interfaces;
using DM.DAL.Repositories;

using Dotmim.Sync.PostgreSql;
using Dotmim.Sync;
using System.Net.Http;
using offline_module.Domain.Interfaces;
using offline_module.Domain.Services;

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
            services.AddMvc();
            //services.AddSingleton<ILogger>();

            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
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
            services.AddScoped<IUserProjectService, UserProjectService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<CurrentUserService>();

            services.AddScoped<ISynchronizationService, SynchronizationService>();
            services.AddScoped<IMinIOService, MinIOService>();
            services.AddScoped<ISyncDotMimService, SyncDotMimService>();
            services.AddScoped<IBearerConfigService, BearerConfigService>();

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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
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
            services.AddAuthorization();

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
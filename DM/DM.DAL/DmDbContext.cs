using System;
using DM.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DM.DAL
{
    public class DmDbContext : DbContext
    {
        public DmDbContext(DbContextOptions<DmDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DmDbContext()
        { }
        public DbSet<UserEntity> Users { get; set; }
        //public DbSet<ObjectiveEntity> Objective { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<RecordEntity> Records { get; set; }
        public DbSet<UserProjectEntity> UsersProjects { get; set; }
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<TemplateEntity> Template { get; set; }
        public DbSet<OrganizationEntity> Organization { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<RoleEntity> Role { get; set; }
        public DbSet<FieldEntity> Field { get; set; }
        public DbSet<ListEntity> List { get; set; }
        public DbSet<ListFieldEntity> ListField { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cascade delete type
            modelBuilder.Entity<ItemEntity>()
                    .HasOne(x => x.Project)
                    .WithMany(x => x.Items)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ListEntity>()
                    .HasOne(x => x.List)
                    .WithMany(x => x.Lists)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FieldEntity>()
                    .HasOne(x => x.Template)
                    .WithMany(x => x.Fields)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ListFieldEntity>()
                    .HasOne(x => x.Template)
                    .WithMany(x => x.ListFields)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FieldEntity>()
                    .HasOne(x => x.Record)
                    .WithMany(x => x.Fields)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ListFieldEntity>()
                    .HasOne(x => x.Record)
                    .WithMany(x => x.ListFields)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PermissionEntity>()
                    .HasOne(x => x.Role)
                    .WithMany(x => x.Permissions)
                    .OnDelete(DeleteBehavior.Cascade);

            // Organization test data
            modelBuilder.Entity<OrganizationEntity>()
                .HasData(new OrganizationEntity
                {
                    Id = 1,
                    Name = "Brio Mrs",
                    Address = "Kazan",
                    Inn = "12345678",
                    Ogrn = "87654321",
                    Kpp = "123456",
                    Phone = "890121212",
                    Email = "qwerty@mail.ru"
                });

            // Role test data
            modelBuilder.Entity<RoleEntity>()
                .HasData(new RoleEntity
                {
                    Id = 1,
                    Name = "Owner",
                });

            modelBuilder.Entity<RoleEntity>()
                .HasData(new RoleEntity
                {
                    Id = 2,
                    Name = "Administrator",
                });

            modelBuilder.Entity<RoleEntity>()
                .HasData(new RoleEntity
                {
                    Id = 3,
                    Name = "Team Supervisor",
                });

            modelBuilder.Entity<RoleEntity>()
                .HasData(new RoleEntity
                {
                    Id = 4,
                    Name = "Supervisor",
                });

            modelBuilder.Entity<RoleEntity>()
                .HasData(new RoleEntity
                {
                    Id = 5,
                    Name = "Worker",
                });
            modelBuilder.Entity<RoleEntity>()
                .HasData(new RoleEntity
                {
                    Id = 6,
                    Name = "User",
                });

            // User test data
            modelBuilder.Entity<UserEntity>()
                .HasData(new UserEntity
                {
                    Id = 1,
                    Name = "admin",
                    LastName = "admin",
                    FathersName = "admin",
                    Login = "string",
                    Email = "string@gamil.com",
                    //pass is - string
                    Password = "AON0utalfV1jyo5nJPnooXEc5NjOWuFBpohmk6xYZ8eK0fjDbSLBGPrY5YkGYlEhBA==",
                    RoleId = 1,
                    Position = "Super Administrator Senior",
                    OrganizationId = 1
                });
            modelBuilder.Entity<UserEntity>()
                .HasData(new UserEntity
                {
                    Id = 2,
                    Name = "TestBot",
                    LastName = "Bot",
                    FathersName = "test",
                    Login = "string1",
                    Email = "string@mail.ru",
                    //pass is - string1
                    Password = "APqcPGe7Q3u2jRDNgHuKrck8E9l1SAEj6knGQqAAZAm3gIoi/E4FJN4lKqEAUwhMLw==",
                    RoleId = 2,
                    Position = "Team Supervisor Junior",
                    OrganizationId = 1
                });

            // Project test data
            modelBuilder.Entity<ProjectEntity>()
                .HasData(new ProjectEntity
                {
                    Id = 1,
                    Title = "Project-1",
                    OrganizationId = 1,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<ProjectEntity>()
                .HasData(new ProjectEntity
                {
                    Id = 2,
                    Title = "Project-2",
                    OrganizationId = 1,
                    CreatedAt = DateTime.Now
                });

            //Record test data
            modelBuilder.Entity<RecordEntity>()
                .HasData(new RecordEntity
                {
                    Id = 1,
                    Name = "Rec-1",
                    ProjectId = 1,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<RecordEntity>()
                .HasData(new RecordEntity
                {
                    Id = 2,
                    Name = "Rec-2",
                    ProjectId = 1,
                    CreatedAt = DateTime.Now
                });

            //Template test data
            modelBuilder.Entity<TemplateEntity>()
                .HasData(new TemplateEntity
                {
                    Id = 1,
                    Name = "Templ-1",
                    ProjectId = 1,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<TemplateEntity>()
                .HasData(new TemplateEntity
                {
                    Id = 2,
                    Name = "Templ-2",
                    ProjectId = 1,
                    CreatedAt = DateTime.Now
                });

            //ListField test data
            modelBuilder.Entity<ListFieldEntity>()
                .HasData(new ListFieldEntity
                {
                    Id = 1,
                    Name = "Status",
                    Type = FieldType.List,
                    IsMandatory = true,
                    CreatedAt = DateTime.Now,
                    TemplateId = 1
                });
            modelBuilder.Entity<ListFieldEntity>()
                .HasData(new ListFieldEntity
                {
                    Id = 2,
                    Name = "Type",
                    Type = FieldType.List,
                    IsMandatory = false,
                    CreatedAt = DateTime.Now,
                    RecordId = 1
                });
            modelBuilder.Entity<ListFieldEntity>()
                .HasData(new ListFieldEntity
                {
                    Id = 3,
                    Name = "Type",
                    Type = FieldType.List,
                    IsMandatory = false,
                    CreatedAt = DateTime.Now,
                    RecordId = 2
                });

            //List test data
            modelBuilder.Entity<ListEntity>()
                .HasData(new ListEntity
                {
                    Id = 1,
                    Data = "Start",
                    CreatedAt = DateTime.Now,
                    ListId = 1
                });
            modelBuilder.Entity<ListEntity>()
                .HasData(new ListEntity
                {
                    Id = 2,
                    Data = "InProgress",
                    CreatedAt = DateTime.Now,
                    ListId = 1
                });
            modelBuilder.Entity<ListEntity>()
                .HasData(new ListEntity
                {
                    Id = 3,
                    Data = "Ready",
                    CreatedAt = DateTime.Now,
                    ListId = 1
                });

            modelBuilder.Entity<ListEntity>()
                .HasData(new ListEntity
                {
                    Id = 4,
                    Data = "Development",
                    CreatedAt = DateTime.Now,
                    ListId = 2
                });
            modelBuilder.Entity<ListEntity>()
                .HasData(new ListEntity
                {
                    Id = 5,
                    Data = "Testing",
                    CreatedAt = DateTime.Now,
                    ListId = 2
                });
            modelBuilder.Entity<ListEntity>()
                .HasData(new ListEntity
                {
                    Id = 6,
                    Data = "Building",
                    CreatedAt = DateTime.Now,
                    ListId = 2
                });

            //Permissions test data
            modelBuilder.Entity<PermissionEntity>()
                .HasData(new PermissionEntity
                {
                    Id = 1,
                    RoleId = 1,
                    Type = PermissionType.Project,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = true,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<PermissionEntity>()
                .HasData(new PermissionEntity
                {
                    Id = 7,
                    RoleId = 1,
                    Type = PermissionType.User,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = true,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<PermissionEntity>()
                .HasData(new PermissionEntity
                {
                    Id = 2,
                    RoleId = 1,
                    Type = PermissionType.Project,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = false,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<PermissionEntity>()
                .HasData(new PermissionEntity
                {
                    Id = 3,
                    RoleId = 2,
                    Type = PermissionType.Project,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = false,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<PermissionEntity>()
                .HasData(new PermissionEntity
                {
                    Id = 4,
                    RoleId = 2,
                    Type = PermissionType.Project,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = true,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<PermissionEntity>()
                .HasData(new PermissionEntity
                {
                    Id = 5,
                    RoleId = 1,
                    Type = PermissionType.Record,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = true,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<PermissionEntity>()
                .HasData(new PermissionEntity
                {
                    Id = 6,
                    RoleId = 1,
                    Type = PermissionType.Template,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = true,
                    CreatedAt = DateTime.Now
                });

            //UserProject
            modelBuilder.Entity<UserProjectEntity>()
                .HasData(new UserProjectEntity
                {
                    Id = 1,
                    ProjectId = 1,
                    UserId = 1,
                    CreatedAt = DateTime.Now,
                });

            //Field test data
            modelBuilder.Entity<FieldEntity>()
                .HasData(new FieldEntity
                {
                    Id = 1,
                    Name = "Description",
                    Type = FieldType.Text,
                    IsMandatory = true,
                    Data = "Editable description",
                    CreatedAt = DateTime.Now,
                    RecordId = 1,
                });
            modelBuilder.Entity<FieldEntity>()
                .HasData(new FieldEntity
                {
                    Id = 2,
                    Name = "Employee",
                    Type = FieldType.Text,
                    IsMandatory = true,
                    Data = "Editable Employee",
                    CreatedAt = DateTime.Now,
                    RecordId = 2,
                });
            modelBuilder.Entity<FieldEntity>()
                .HasData(new FieldEntity
                {
                    Id = 3,
                    Name = "Description",
                    Type = FieldType.Text,
                    IsMandatory = true,
                    Data = "Editable description",
                    CreatedAt = DateTime.Now,
                    TemplateId = 1,
                });
            modelBuilder.Entity<FieldEntity>()
                .HasData(new FieldEntity
                {
                    Id = 4,
                    Name = "Estimate",
                    Type = FieldType.Text,
                    IsMandatory = true,
                    Data = "Editable Estimate",
                    CreatedAt = DateTime.Now,
                    TemplateId = 2,
                });
        }
    }
}

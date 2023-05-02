using System;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Enums;

namespace DM.DAL
{
    public class DmDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        //public DbSet<ObjectiveEntity> Objective { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<UserProject> UsersProjects { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Template> Template { get; set; }
        //public DbSet<RecordTemplateEntity> RecordTemplate { get; set; }
        //public DbSet<DocumentTemplateEntity> DocumentTemplate { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Field> Field { get; set; }
        public DbSet<List> List { get; set; }
        public DbSet<ListField> ListField { get; set; }

        public DmDbContext(DbContextOptions<DmDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DmDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cascade delete type
            modelBuilder.Entity<Item>()
                    .HasOne(x => x.Project)
                    .WithMany(x => x.Items)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<List>()
                    .HasOne(x => x.ListField)
                    .WithMany(x => x.Lists)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Field>()
                    .HasOne(x => x.Template)
                    .WithMany(x => x.Fields)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ListField>()
                    .HasOne(x => x.Template)
                    .WithMany(x => x.ListFields)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Field>()
                    .HasOne(x => x.Record)
                    .WithMany(x => x.Fields)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ListField>()
                    .HasOne(x => x.Record)
                    .WithMany(x => x.ListFields)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Permission>()
                    .HasOne(x => x.Role)
                    .WithMany(x => x.Permissions)
                    .OnDelete(DeleteBehavior.Cascade);

            // Organization test data
            modelBuilder.Entity<Organization>()
                .HasData(new Organization
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
            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = 1,
                    Name = "Owner",
                });

            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = 2,
                    Name = "Administrator",
                });

            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = 3,
                    Name = "Team Supervisor",
                });

            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = 4,
                    Name = "Supervisor",
                });

            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = 5,
                    Name = "Worker",
                });
            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = 6,
                    Name = "User",
                });

            // User test data
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    Name = "admin",
                    LastName = "admin",
                    FathersName = "admin",
                    Login = "string",
                    Email = "string@gamil.com",
                    //pass is - string
                    HashedPassword = "AON0utalfV1jyo5nJPnooXEc5NjOWuFBpohmk6xYZ8eK0fjDbSLBGPrY5YkGYlEhBA==",
                    RoleId = 1,
                    Position = "Super Administrator Senior",
                    OrganizationId = 1
                });
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 2,
                    Name = "TestBot",
                    LastName = "Bot",
                    FathersName = "test",
                    Login = "string1",
                    Email = "string@mail.ru",
                    //pass is - string1
                    HashedPassword = "APqcPGe7Q3u2jRDNgHuKrck8E9l1SAEj6knGQqAAZAm3gIoi/E4FJN4lKqEAUwhMLw==",
                    RoleId = 2,
                    Position = "Team Supervisor Junior",
                    OrganizationId = 1
                });

            // Project test data
            modelBuilder.Entity<Project>()
                .HasData(new Project
                {
                    Id = 1,
                    Title = "Project-1",
                    OrganizationId = 1,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<Project>()
                .HasData(new Project
                {
                    Id = 2,
                    Title = "Project-2",
                    OrganizationId = 1,
                    CreatedAt = DateTime.Now
                });

            //Record test data
            modelBuilder.Entity<Record>()
                .HasData(new Record
                {
                    Id = 1,
                    Name = "Rec-1",
                    ProjectId = 1,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<Record>()
                .HasData(new Record
                {
                    Id = 2,
                    Name = "Rec-2",
                    ProjectId = 1,
                    CreatedAt = DateTime.Now
                });

            //Template test data
            modelBuilder.Entity<Template>()
                .HasData(new Template
                {
                    Id = 1,
                    Name = "Templ-1",
                    ProjectId = 1,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<Template>()
                .HasData(new Template
                {
                    Id = 2,
                    Name = "Templ-2",
                    ProjectId = 1,
                    CreatedAt = DateTime.Now
                });

            //ListField test data
            modelBuilder.Entity<ListField>()
                .HasData(new ListField
                {
                    Id = 1,
                    Name = "Status",
                    Type = FieldEnum.List,
                    IsMandatory = true,
                    CreatedAt = DateTime.Now,
                    TemplateId = 1
                });
            modelBuilder.Entity<ListField>()
                .HasData(new ListField
                {
                    Id = 2,
                    Name = "Type",
                    Type = FieldEnum.List,
                    IsMandatory = false,
                    CreatedAt = DateTime.Now,
                    RecordId = 1
                });
            modelBuilder.Entity<ListField>()
                .HasData(new ListField
                {
                    Id = 3,
                    Name = "Type",
                    Type = FieldEnum.List,
                    IsMandatory = false,
                    CreatedAt = DateTime.Now,
                    RecordId = 2
                });

            //List test data
            modelBuilder.Entity<List>()
                .HasData(new List
                {
                    Id = 1,
                    Data = "Start",
                    CreatedAt = DateTime.Now,
                    ListId = 1
                });
            modelBuilder.Entity<List>()
                .HasData(new List
                {
                    Id = 2,
                    Data = "InProgress",
                    CreatedAt = DateTime.Now,
                    ListId = 1
                });
            modelBuilder.Entity<List>()
                .HasData(new List
                {
                    Id = 3,
                    Data = "Ready",
                    CreatedAt = DateTime.Now,
                    ListId = 1
                });

            modelBuilder.Entity<List>()
                .HasData(new List
                {
                    Id = 4,
                    Data = "Development",
                    CreatedAt = DateTime.Now,
                    ListId = 2
                });
            modelBuilder.Entity<List>()
                .HasData(new List
                {
                    Id = 5,
                    Data = "Testing",
                    CreatedAt = DateTime.Now,
                    ListId = 2
                });
            modelBuilder.Entity<List>()
                .HasData(new List
                {
                    Id = 6,
                    Data = "Building",
                    CreatedAt = DateTime.Now,
                    ListId = 2
                });

            //Permissions test data
            modelBuilder.Entity<Permission>()
                .HasData(new Permission
                {
                    Id = 1,
                    RoleId = 1,
                    Type = PermissionEnum.Project,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = true,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<Permission>()
                .HasData(new Permission
                {
                    Id = 7,
                    RoleId = 1,
                    Type = PermissionEnum.User,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = true,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<Permission>()
                .HasData(new Permission
                {
                    Id = 2,
                    RoleId = 1,
                    Type = PermissionEnum.Project,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = false,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<Permission>()
                .HasData(new Permission
                {
                    Id = 3,
                    RoleId = 2,
                    Type = PermissionEnum.Project,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = false,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<Permission>()
                .HasData(new Permission
                {
                    Id = 4,
                    RoleId = 2,
                    Type = PermissionEnum.Project,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = true,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<Permission>()
                .HasData(new Permission
                {
                    Id = 5,
                    RoleId = 1,
                    Type = PermissionEnum.Record,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = true,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<Permission>()
                .HasData(new Permission
                {
                    Id = 6,
                    RoleId = 1,
                    Type = PermissionEnum.Template,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = true,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<Permission>()
                .HasData(new Permission
                {
                    Id = 8,
                    RoleId = 1,
                    Type = PermissionEnum.Role,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = true,
                    CreatedAt = DateTime.Now
                });

            //UserProject
            modelBuilder.Entity<UserProject>()
                .HasData(new UserProject
                {
                    Id = 1,
                    ProjectId = 1,
                    UserId = 1,
                    CreatedAt = DateTime.Now,
                });

            //Field test data
            modelBuilder.Entity<Field>()
                .HasData(new Field
                {
                    Id = 1,
                    Name = "Description",
                    Type = FieldEnum.Text,
                    IsMandatory = true,
                    Data = "Editable description",
                    CreatedAt = DateTime.Now,
                    RecordId = 1,
                });
            modelBuilder.Entity<Field>()
                .HasData(new Field
                {
                    Id = 2,
                    Name = "Employee",
                    Type = FieldEnum.Text,
                    IsMandatory = true,
                    Data = "Editable Employee",
                    CreatedAt = DateTime.Now,
                    RecordId = 2,
                });
            modelBuilder.Entity<Field>()
                .HasData(new Field
                {
                    Id = 3,
                    Name = "Description",
                    Type = FieldEnum.Text,
                    IsMandatory = true,
                    Data = "Editable description",
                    CreatedAt = DateTime.Now,
                    TemplateId = 1,
                });
            modelBuilder.Entity<Field>()
                .HasData(new Field
                {
                    Id = 4,
                    Name = "Estimate",
                    Type = FieldEnum.Text,
                    IsMandatory = true,
                    Data = "Editable Estimate",
                    CreatedAt = DateTime.Now,
                    TemplateId = 2,
                });
        }
    }
}

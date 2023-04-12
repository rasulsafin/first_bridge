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
        //      public DbSet<ObjectiveEntity> Objective { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<RecordEntity> Records { get; set; }
        //      public DbSet<UserProjectEntity> UserProjects { get; set; }
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
            // Users should have unique logins
            modelBuilder.Entity<UserEntity>()
                .HasIndex(x => x.Login)
                .IsUnique(true);

            modelBuilder.Entity<UserEntity>()
                .HasIndex(x => x.Email)
                .IsUnique(true);

            modelBuilder.Entity<OrganizationEntity>()
                .HasIndex(x => x.Inn)
                .IsUnique(true);

            modelBuilder.Entity<OrganizationEntity>()
                .HasIndex(x => x.Name)
                .IsUnique(true);

            modelBuilder.Entity<OrganizationEntity>()
                .HasIndex(x => x.Ogrn)
                .IsUnique(true);

            modelBuilder.Entity<OrganizationEntity>()
                .HasIndex(x => x.Email)
                .IsUnique(true);

            modelBuilder.Entity<ProjectEntity>()
                .HasMany(c => c.Template)
                .WithOne(e => e.Project)
                .IsRequired();

            modelBuilder.Entity<OrganizationEntity>()
                .HasMany(c => c.Projects)
                .WithOne(e => e.Organization)
                .IsRequired();

            modelBuilder.Entity<OrganizationEntity>()
                .HasMany(c => c.Users)
                .WithOne(e => e.Organization)
                .IsRequired();

            modelBuilder.Entity<ItemEntity>()
                    .HasOne(x => x.Project)
                    .WithMany(x => x.Items)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ListEntity>()
                    .HasOne(x => x.List)
                    .WithMany(x => x.Lists);

            modelBuilder.Entity<FieldEntity>()
                    .HasOne(x => x.Template)
                    .WithMany(x => x.Fields);

            modelBuilder.Entity<ListFieldEntity>()
                    .HasOne(x => x.Template)
                    .WithMany(x => x.ListFields);

            modelBuilder.Entity<FieldEntity>()
                    .HasOne(x => x.Record)
                    .WithMany(x => x.Fields);

            modelBuilder.Entity<ListFieldEntity>()
                    .HasOne(x => x.Record)
                    .WithMany(x => x.ListFields);

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
                    Roles = "Admin",
                    Birthdate = DateTime.Now,
                    Snils = "snils111",
                    Position = "admin",
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
                    Roles = "Admin",
                    Birthdate = DateTime.Now,
                    Snils = "snils123",
                    Position = "admin",
                    OrganizationId = 1
                });

            // Project test data
            modelBuilder.Entity<ProjectEntity>()
                .HasData(new ProjectEntity
                {
                    Id = 1,
                    Title = "Project-1",
                    Description = "Proj 1",
                    OrganizationId = 1,
                    CreatedAt = DateTime.Now
                });
            modelBuilder.Entity<ProjectEntity>()
                .HasData(new ProjectEntity
                {
                    Id = 2,
                    Title = "Project-2",
                    Description = "Proj 2",
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
                    UserId = 1,
                    ObjectId = 1,
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
                    Id = 2,
                    UserId = 1,
                    ObjectId = 2,
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
                    UserId = 2,
                    ObjectId = 1,
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
                    UserId = 2,
                    ObjectId = 2,
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
                    UserId = 1,
                    ObjectId = 1,
                    Type = PermissionType.Record,
                    Create = true,
                    Read = true,
                    Update = true,
                    Delete = true,
                    CreatedAt = DateTime.Now
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

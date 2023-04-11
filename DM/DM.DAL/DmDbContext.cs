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

            modelBuilder.Entity<TemplateEntity>()
                .HasMany(c => c.Fields)
                .WithMany(e => e.Template)
                .UsingEntity<TemplateFieldEntity>(
                    j => j
                    .HasOne(pt => pt.Field)
                    .WithMany(t => t.TemplateField)
                    .HasForeignKey(pt => pt.FieldId),
                    j => j
                    .HasOne(pt => pt.Template)
                    .WithMany(t => t.TemplateField)
                    .HasForeignKey(pt => pt.TemplateId),
                    j =>
                    {
                        j.HasKey(t => new { t.Id });
                        j.ToTable("TemplateField");
                    }
                 );

            modelBuilder.Entity<RecordEntity>()
                .HasMany(c => c.Fields)
                .WithMany(e => e.Record)
                .UsingEntity<RecordFieldEntity>(
                    j => j
                    .HasOne(pt => pt.Field)
                    .WithMany(t => t.RecordField)
                    .HasForeignKey(pt => pt.FieldId),
                    j => j
                    .HasOne(pt => pt.Record)
                    .WithMany(t => t.RecordField)
                    .HasForeignKey(pt => pt.RecordId),
                    j =>
                    {
                        j.HasKey(t => new { t.Id });
                        j.ToTable("RecordField");
                    }
                 );

            modelBuilder.Entity<ListEntity>()
                .HasMany(c => c.Field)
                .WithMany(e => e.List)
                .UsingEntity<ListFieldEntity>(
                    j => j
                    .HasOne(pt => pt.Field)
                    .WithMany(t => t.ListField)
                    .HasForeignKey(pt => pt.FieldId),
                    j => j
                    .HasOne(pt => pt.List)
                    .WithMany(t => t.ListField)
                    .HasForeignKey(pt => pt.ListId),
                    j =>
                    {
                        j.HasKey(t => new { t.Id });
                        j.ToTable("ListField");
                    }
                 );

            // TODO Delete later
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

            // TODO Delete later
            modelBuilder.Entity<UserEntity>()
                .HasData(new UserEntity
                {
                    Id = 1,
                    Name = "admin",
                    LastName = "admin",
                    FathersName = "admin",
                    Login = "string",
                    Email = "string",
                    Password = "string",
                    Roles = "Admin",
                    Birthdate = DateTime.Now,
                    Snils = "string",
                    Position = "admin",
                    OrganizationId = 1
                });

            // TODO Delete later
            modelBuilder.Entity<ProjectEntity>()
                .HasData(new ProjectEntity
                {
                    Id = 1,
                    Title = "title1",
                    Description = "description",
                    OrganizationId = 1,
                    CreatedAt = DateTime.Now
                });
        }
    }
}

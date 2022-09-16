using DM.DAL.Entities;
using DM.Entities;
using Microsoft.EntityFrameworkCore;

namespace DM.repository
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
        public DbSet<ObjectiveEntity> Objective { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<RecordEntity> Records { get; set; }
        public DbSet<UserProjectEntity> UserProjects { get; set; }
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<TemplateEntity> Template { get; set; }
        public DbSet<OrganizationEntity> Organization { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecordEntity>()
                .Property(b => b.Fields)
                .HasColumnType("jsonb");
            
            // Users should have unique logins
            modelBuilder.Entity<UserEntity>()
                .HasIndex(x => x.Login)
                .IsUnique(true);

            modelBuilder.Entity<OrganizationEntity>()
                .HasIndex(x => x.Inn)
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
        }
    }
}

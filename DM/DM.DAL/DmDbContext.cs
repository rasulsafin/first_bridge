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

        public void Detach<T>(DbContext context, T entry)
        {
            if (entry == null)
            {
                return;
            }

            context.Entry(entry).State = EntityState.Detached;
        }

        public DmDbContext()
        { }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ObjectiveEntity> Objective { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<RecordEntity> Records { get; set; }
        public DbSet<UserProjectEntity> UserProjects { get; set; }
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users should have unique logins
            
            modelBuilder.Entity<UserEntity>()
                .HasIndex(x => x.Login)
                .IsUnique(true);
            /*
               modelBuilder.Entity<UserProjectEntity>()
                   .HasKey(x => new { x.ProjectId, x.UserId });
               modelBuilder.Entity<UserProjectEntity>()
                   .HasOne(x => x.User)
                   .WithMany(x => x.Projects)
                   .OnDelete(DeleteBehavior.Cascade);
               modelBuilder.Entity<UserProjectEntity>()
                   .HasOne(x => x.Project)
                   .WithMany(x => x.Users)
                   .OnDelete(DeleteBehavior.Cascade);
            */

            modelBuilder.Entity<ItemEntity>()
                    .HasOne(x => x.Project)
                    .WithMany(x => x.Items)
                    .OnDelete(DeleteBehavior.Cascade);
            /*
                        modelBuilder.Entity<FieldsEntity>().
                            HasOne(x => x.AssigneeId)
                            .WithMany(x => x.Fields)
                            .OnDelete(DeleteBehavior.Cascade);
            

            public static void Detach<T>(this DbContext context, T entry)
            {
                if (entry == null)
                {
                    return;
                }

                context.Entry(entry).State = EntityState.Detached;
            }
            */
        }

    }
}

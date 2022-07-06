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
        {

        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<FieldsEntity> Fields { get; set; }
        public DbSet<ObjectiveEntity> Objective { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<RecordEntity> Records { get; set; }
        public DbSet<UserProjectEntity> UserProjects { get; set; }
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        
    }

}

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
        public DbSet<ProjectEntity> Project { get; set; }
        public DbSet<RecordEntity> Record { get; set; }
        public DbSet<UserProjectEntity> UserProject { get; set; }
        
    }

}

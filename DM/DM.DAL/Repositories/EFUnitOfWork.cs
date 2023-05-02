using System;
using System.Threading.Tasks;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DmDbContext _dbContext;
        private UserRepository userRepository;

        private bool disposed = false;

        public IUserRepository<User> Users
        {
            get
            {
                userRepository ??= new UserRepository(_dbContext);
                return userRepository;
            }
        }

        public EFUnitOfWork(DmDbContext context)
        {
            _dbContext = context;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

using System;
using System.Threading.Tasks;
using DM.DAL.Entities;

namespace DM.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository<User> Users { get; }
        void Save();
        Task SaveAsync();
    }
}

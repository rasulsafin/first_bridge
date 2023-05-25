using System.Threading.Tasks;

using DM.DAL.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DM.DAL.Interfaces
{
    public interface IUserRepository<T> : IRepository<User>
    {
        Task<User> Authenticate(string login);
        
        Task<EntityEntry<User>> CreateUserWithProjects(T item);
    }
}

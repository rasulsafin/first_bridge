using System.Threading.Tasks;

using DM.DAL.Entities;

namespace DM.DAL.Interfaces
{
    public interface IUserRepository<T> : IRepository<User>
    {
        abstract Task<T> Authenticate(string login);
    }
}

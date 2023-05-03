using DM.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DM.DAL.Interfaces
{
    public interface IItemRepository<T> : IRepository<Item>
    {
        Task<IEnumerable<T>> GetAllByProject(long id);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DM.DAL.Entities;

namespace DM.DAL.Interfaces
{
    public interface IItemRepository<T> : IRepository<Item>
    {
        Task<IEnumerable<T>> GetAllByProject(long id);
        T Find(string fileName);
    }
}

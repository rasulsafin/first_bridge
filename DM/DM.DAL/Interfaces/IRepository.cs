using System.Collections.Generic;
using System.Threading.Tasks;

namespace DM.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        abstract Task<IEnumerable<T>> GetAll();
        abstract T GetById(long? id);
        abstract Task<bool> Create(T item);
        abstract void Update(T item);
        abstract bool Delete(long? id);
    }
}

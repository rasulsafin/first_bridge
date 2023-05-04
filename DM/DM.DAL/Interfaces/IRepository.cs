using System.Collections.Generic;
using System.Threading.Tasks;

namespace DM.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        T GetById(long? id);
        Task<bool> Create(T item);
        void Update(T item);
        bool Delete(long? id);
    }
}

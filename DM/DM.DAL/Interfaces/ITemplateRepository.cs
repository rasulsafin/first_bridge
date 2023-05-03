using System.Collections.Generic;
using System.Threading.Tasks;

using DM.DAL.Entities;

namespace DM.DAL.Interfaces
{
    public interface ITemplateRepository<T> : IRepository<Template>
    {
        Task<IEnumerable<Template>> GetAllOfProject(long? id);
    }
}

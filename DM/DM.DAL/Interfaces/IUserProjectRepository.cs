using System.Collections.Generic;
using System.Threading.Tasks;

using DM.DAL.Entities;

namespace DM.DAL.Interfaces
{
    public interface IUserProjectRepository<T> : IRepository<UserProject>
    {
        Task<bool> AddToProjects(IEnumerable<UserProject> userProjects);
        Task<bool> DeleteFromProject(long userId, long projectId);
        Task<bool> IsExist(long userId, long projectId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IUserProjectService
    {
        public Task<bool> AddToProject(UserProjectModel userProjectModel);
        public Task<bool> AddToProjects(List<UserProjectModel> userProjectsModel);
        public Task<bool> DeleteFromProject(long userId, long projectId);
    }
}

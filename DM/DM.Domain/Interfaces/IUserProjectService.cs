using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IUserProjectService
    {
        public Task<bool> AddToProject(UserProjectDto userProjectModel);
        public Task<bool> AddToProjects(List<UserProjectDto> userProjectsModel);
        public Task<bool> DeleteFromProject(long userId, long projectId);
        void Dispose();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface IUserProjectService : IGetAccess
    {
        public Task<bool> AddToProject(UserProjectDto userProject);
        public Task<bool> AddToProjects(List<UserProjectDto> userProjects);
        public Task<bool> DeleteFromProject(long userId, long projectId);
        void Dispose();
    }
}

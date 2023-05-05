using DM.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IProjectService : IGetAccess
    {
        public Task<IEnumerable<ProjectForReadDto>> GetAll();
        public ProjectForReadDto GetById(long? projectId);
        public Task<long> Create(ProjectForReadDto project);
        public Task<bool> Update(ProjectForUpdateDto project);
        public Task<bool> Archive(long? projectId);
        void Dispose();
    }
}

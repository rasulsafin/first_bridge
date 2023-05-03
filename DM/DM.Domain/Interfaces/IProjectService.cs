using DM.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IProjectService
    {
        public Task<IEnumerable<ProjectForReadDto>> GetAll();
        public ProjectForReadDto GetById(long? projectId);
        public Task<long> Create(ProjectForReadDto projectModel);
        public Task<bool> Update(ProjectForUpdateDto projectModel);
        public Task<bool> Archive(long? projectId);
    }
}

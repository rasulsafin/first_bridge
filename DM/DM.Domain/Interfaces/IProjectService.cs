using DM.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IProjectService
    {
        public Task<List<ProjectForReadModel>> GetAll();
        public Task<ProjectForReadModel> GetById(long projectId);
        public Task<long> Create(ProjectForReadModel projectModel);
        public Task<bool> Update(ProjectForUpdateModel projectModel);
        public Task<bool> Delete(long projectId);
    }
}

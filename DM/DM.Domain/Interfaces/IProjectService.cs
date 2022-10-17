using DM.Domain.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IProjectService
    {
        public Task<List<ProjectModel>> GetAll();
        public Task<ProjectModel> GetById(long projectId);
        public Task<long> Create(ProjectModel projectModel);
        public Task<bool> Update(ProjectModel projectModel);
        public Task<bool> Delete(long projectId);
    }
}

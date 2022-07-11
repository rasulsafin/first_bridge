using DM.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IProjectService
    {
        public Task<List<ProjectModel>> GetAll();
        public ProjectModel GetById(long projectId);
        public Task<long> Create(ProjectModel projectModel);
    }
}

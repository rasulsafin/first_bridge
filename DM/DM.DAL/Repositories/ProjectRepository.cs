using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DM.DAL.Repositories
{
    public class ProjectRepository : IProjectRepository<Project>
    {
        private readonly DmDbContext _dbContext;

        public ProjectRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            IEnumerable<Project> projects = await _dbContext.Projects
                .Include(x => x.Templates)
                .Include(x => x.Items)
                .Include(x => x.UserProjects).ThenInclude(y => y.User)
                .OrderBy(x => x.IsInArchive)
                .ToListAsync();

            return projects;
        }

        public Project GetById(long? id)
        {
            Project project = _dbContext.Projects
                .Include(x => x.Templates)
                .Include(x => x.Items)
                .Include(x => x.UserProjects).ThenInclude(y => y.User)
                .FirstOrDefault(y => y.Id == id);

            return project;
        }

        public async Task<bool> Create(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            return true;
        }
        
        public async Task<EntityEntry<Project>> CreateProjectWithUsers(Project project)
        {
            var result = await _dbContext.Projects.AddAsync(project);
            return result;
        }

        public void Update(Project project)
        {
            _dbContext.Entry(project).State = EntityState.Modified;
        }

        public bool Delete(long? id)
        {
            Project project = _dbContext.Projects.Find(id);
            if (project != null)
            {
                _dbContext.Projects.Remove(project);
                return true;
            }
            return false;
        }

        public bool Archive(long? id)
        {
            Project project = _dbContext.Projects.Find(id);
            if (project != null)
            {
                project.IsInArchive = true;
                _dbContext.Projects.Update(project);
                return true;
            }
            return false;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.DAL.Repositories
{
    public class UserProjectRepository : IUserProjectRepository<UserProject>
    {
        private readonly DmDbContext _dbContext;

        public UserProjectRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddToProjects(IEnumerable<UserProject> userProjects)
        {
            await _dbContext.UsersProjects.AddRangeAsync(userProjects);
            return true;
        }

        public async Task<bool> Create(UserProject userProject)
        {
            await _dbContext.UsersProjects.AddAsync(userProject);
            return true;
        }

        public async Task<bool> DeleteFromProject(long userId, long projectId)
        {
            UserProject userProject = await _dbContext.UsersProjects.FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId);
            if (userProject != null)
            {
                _dbContext.UsersProjects.Remove(userProject);
                return true;
            }
            return false;
        }

        public async Task<bool> IsExist(long userId, long projectId)
        {
            UserProject userProject = await _dbContext.UsersProjects.FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId);
            return userProject != null;
        }

        public async Task<IEnumerable<UserProject>> GetAll()
        {
            IEnumerable<UserProject> userProjects = await _dbContext.UsersProjects.ToListAsync();
            return userProjects;
        }

        public UserProject GetById(long? id)
        {
            UserProject userProject = _dbContext.UsersProjects.FirstOrDefault(y => y.Id == id);
            return userProject;
        }

        public void Update(UserProject userProject)
        {
            _dbContext.Entry(userProject).State = EntityState.Modified;
        }

        public bool Delete(long? id)
        {
            UserProject userProject = _dbContext.UsersProjects.Find(id);
            if (userProject != null)
            {
                _dbContext.UsersProjects.Remove(userProject);
                return true;
            }
            return false;
        }
    }
}

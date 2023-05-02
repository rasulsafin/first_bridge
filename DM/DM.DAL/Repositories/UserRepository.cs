using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

namespace DM.DAL.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly DmDbContext _dbContext;

        public UserRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Authenticate(string login)
        {
            User user = await _dbContext.Users.Include(x => x.Role)
                .FirstOrDefaultAsync(x => (x.Login == login || x.Email == login));

            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            IEnumerable<User> users = await _dbContext.Users.Include(x => x.UserProjects).ThenInclude(y => y.Project)
                .Include(x => x.Role)
                .ToListAsync();

            return users;
        }

        public User GetById(long? id)
        {
            User user = _dbContext.Users.Include(x => x.UserProjects).ThenInclude(y => y.Project)
                .Include(x => x.Role)
                .FirstOrDefault(y => y.Id == id);

            return user;
        }

        public async Task<bool> Create(User user)
        {
            await _dbContext.Users.AddAsync(user);
            return true;
        }

        public void Update(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
        }

        public bool Delete(long? id)
        {
            User user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                return true;
            }
            return false;
        }
    }
}

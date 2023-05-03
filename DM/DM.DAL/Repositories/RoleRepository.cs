using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;


namespace DM.DAL.Repositories
{
    public class RoleRepository : IRoleRepository<Role>
    {
        private readonly DmDbContext _dbContext;

        public RoleRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Role role)
        {
            await _dbContext.Role.AddAsync(role);
            return true;
        }

        public bool Delete(long? id)
        {
            Role role = _dbContext.Role.Find(id);
            if (role != null)
            {
                _dbContext.Role.Remove(role);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            IEnumerable<Role> roles = await _dbContext.Role
                .Include(x => x.Permissions)
                .ToListAsync();

            return roles;
        }

        public Role GetById(long? id)
        {
            Role role = _dbContext.Role
                .Include(x => x.Permissions)
                .FirstOrDefault(x => x.Id == id);

            return role;
        }

        public void Update(Role role)
        {
            _dbContext.Entry(role).State = EntityState.Modified;
        }
    }
}

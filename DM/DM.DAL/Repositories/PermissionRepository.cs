using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;
using DM.DAL.Enums;

namespace DM.DAL.Repositories
{
    public class PermissionRepository : IPermissionRepository<Permission>
    {
        private readonly DmDbContext _dbContext;

        public PermissionRepository(DmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Permission>> GetAllByRole(long roleId)
        {
            IEnumerable<Permission> permissions = await _dbContext.Permissions
                .Where(x => x.RoleId == roleId)
                .ToListAsync();

            return permissions;
        }

        public async Task<Permission> GetByRoleAndType(long roleId, PermissionEnum permission)
        {
            Permission permissions = await _dbContext.Permissions
                .FirstOrDefaultAsync(x => x.RoleId == roleId && x.Type == permission);

            return permissions;
        }

        public void Update(Permission permission)
        {
            _dbContext.Entry(permission).State = EntityState.Modified;
        }

        public Task<bool> Create(Permission item)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(long? id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Permission GetById(long? id)
        {
            throw new System.NotImplementedException();
        }
    }
}

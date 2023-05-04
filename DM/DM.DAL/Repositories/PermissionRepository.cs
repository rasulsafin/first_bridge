using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

using DM.Common.Enums;

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

        public async Task<bool> Create(Permission permission)
        {
            await _dbContext.Permissions.AddAsync(permission);
            return true;
        }

        public bool Delete(long? id)
        {
            Permission permission = _dbContext.Permissions.Find(id);
            if (permission != null)
            {
                _dbContext.Permissions.Remove(permission);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Permission>> GetAll()
        {
            IEnumerable<Permission> users = await _dbContext.Permissions.ToListAsync();
            return users;
        }

        public Permission GetById(long? id)
        {
            Permission permission = _dbContext.Permissions.FirstOrDefault(y => y.Id == id);
            return permission;
        }
    }
}

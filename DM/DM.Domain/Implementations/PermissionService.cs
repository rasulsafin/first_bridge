using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL;

namespace DM.Domain.Implementations
{
    public class PermissionService : IPermissionService
    {
        private readonly DmDbContext _context;

        public PermissionService(DmDbContext context)
        {
            _context = context;
        }

        public async Task<List<PermissionEntity>> GetAllByRole(long roleId)
        {
            var result = await _context.Permissions.Where(x => x.RoleId == roleId).ToListAsync();

            return result;
        }

        public async Task<bool> UpdatePermissionOnRole(PermissionModel permissionModel)
        {
            var permission = await _context.Permissions.FirstOrDefaultAsync(x => x.RoleId == permissionModel.RoleId && x.Type == permissionModel.Type);

            if (permission == null) return false;

            _context.Permissions.Attach(permission);

            permission.Type = permissionModel.Type;
            permission.Create = permissionModel.Create;
            permission.Read = permissionModel.Read;
            permission.Update = permissionModel.Update;
            permission.Delete = permissionModel.Delete;
            permission.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _context.Entry(permission).State = EntityState.Detached;

            return true;
        }
    }
}

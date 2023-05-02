using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL;
using AutoMapper;

namespace DM.Domain.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly DmDbContext _context;

        private readonly IMapper _mapper;

        public PermissionService(DmDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PermissionDto>> GetAllByRole(long roleId)
        {
            var permissions = await _context.Permissions.Where(x => x.RoleId == roleId).ToListAsync();

            return _mapper.Map<List<PermissionDto>>(permissions);
        }

        public async Task<bool> UpdatePermissionOnRole(PermissionDto permissionModel)
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

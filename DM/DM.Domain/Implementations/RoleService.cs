using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL;
using DM.DAL.Entities;
using System;

namespace DM.Domain.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly DmDbContext _context;

        private readonly IMapper _mapper;

        public RoleService(DmDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<RoleModel> GetAll()
        {
            var roles = _context.Role.ToList();

            return _mapper.Map<List<RoleModel>>(roles);
        }

        public RoleModel GetById(long roleId)
        {
            var role = _context.Role.Include(x => x.Permissions)
                                    .FirstOrDefault(x => x.Id == roleId);

            if (role == null)
            {
                return null;
            }

            return _mapper.Map<RoleModel>(role);
        }

        public async Task<bool> Create(RoleForCreateModel roleForCreateModel)
        {
            var role = _mapper.Map<RoleEntity>(new RoleModel
            {
                Name = roleForCreateModel.Name,
                Description = roleForCreateModel.Description,
                Permissions = roleForCreateModel.Permissions,
            });

            _context.Role.Add(role);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Update(RoleForUpdateModel roleForUpdateModel)
        {
            var role = _context.Role.FirstOrDefault(x => x.Id == roleForUpdateModel.Id);

            if (role == null) return false;

            _context.Role.Attach(role);

            role.Name = roleForUpdateModel.Name;
            role.Description = roleForUpdateModel.Description;
            role.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _context.Entry(role).State = EntityState.Detached;

            return true;
        }
        public async Task<bool> Delete(long roleId)
        {
            var role = await _context.Role
                .Include(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == roleId);

            if (role == null) return false;

            _context.Role.Remove(role);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

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

        public async Task<bool> Create(RoleForCreateModel roleModel)
        {
            var role = _mapper.Map<RoleEntity>(new RoleModel
            {
                Name = roleModel.Name,
                Description = roleModel.Description,
                Permissions = roleModel.Permissions,
            });

            _context.Role.Add(role);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Update(RoleForUpdateModel roleModel)
        {
            var roleForUpdate = _context.Role.FirstOrDefault(x => x.Id == roleModel.Id);

            if (roleForUpdate == null)
            {
                return false;
            }

            _context.Role.Attach(roleForUpdate);

            roleForUpdate.Name = roleForUpdate.Name;
            roleForUpdate.Description = roleForUpdate.Description;

            await _context.SaveChangesAsync();

            _context.Entry(roleForUpdate).State = EntityState.Detached;

            return true;
        }
        public async Task<bool> Delete(long roleId)
        {
            var role = await _context.Role
                .Include(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == roleId);

            if (role == null)
            {
                return false;
            }

            _context.Role.Remove(role);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.DTO;

using DM.DAL.Entities;
using DM.DAL.Interfaces;
using DM.Common.Enums;

namespace DM.Domain.Services
{
    public class RoleService : IRoleService
    {
        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Context = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAll()
        {
            var roles = await Context.Roles.GetAll();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public RoleDto GetById(long roleId)
        {
            if (roleId < 1) return null;

            var role = Context.Roles.GetById(roleId);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<bool> Create(RoleForCreateDto roleForCreateDto)
        {
            var role = _mapper.Map<Role>(new RoleDto
            {
                Name = roleForCreateDto.Name,
                Description = roleForCreateDto.Description,
                Permissions = roleForCreateDto.Permissions,
            });

            await Context.Roles.Create(role);
            await Context.SaveAsync();

            return true;
        }
        public async Task<bool> Update(RoleForUpdateDto roleForUpdateDto)
        {
            var role = Context.Roles.GetById(roleForUpdateDto.Id);

            if (role == null) return false;

            role.Name = roleForUpdateDto.Name;
            role.Description = roleForUpdateDto.Description;
            role.UpdatedAt = DateTime.UtcNow;

            Context.Roles.Update(role);
            await Context.SaveAsync();

            return true;
        }
        public async Task<bool> Delete(long roleId)
        {
            var result = Context.Roles.Delete(roleId);
            await Context.SaveAsync();

            return result;
        }

        public async Task<PermissionDto> GetAccess(long roleId, PermissionEnum permission)
        {
            var access = await Context.Permissions.GetByRoleAndType(roleId, permission);
            return _mapper.Map<PermissionDto>(access);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

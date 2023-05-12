using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.DTO;

using DM.DAL.Interfaces;
using DM.Common.Enums;

namespace DM.Domain.Services
{
    public class PermissionService : IPermissionService
    {
        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;

        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Context = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionDto>> GetAllByRole(long roleId)
        {
            if (roleId < 1) return null;

            var permissions = await Context.Permissions.GetAllByRole(roleId);
            return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }

        public async Task<bool> UpdatePermissionOnRole(PermissionDto permissionDto)
        {
            var permission = await Context.Permissions.GetByRoleAndType(permissionDto.RoleId, permissionDto.Type);

            if (permission == null) return false;

            permission.Type = permissionDto.Type;
            permission.Create = permissionDto.Create;
            permission.Read = permissionDto.Read;
            permission.Update = permissionDto.Update;
            permission.Delete = permissionDto.Delete;
            permission.UpdatedAt = DateTime.UtcNow;

            Context.Permissions.Update(permission);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> GetAccess(long roleId, ActionEnum action)
        {
            try
            {
                var access = await Context.Permissions.GetByRoleAndType(roleId, PermissionEnum.Role);

                return action switch
                {
                    ActionEnum.Read => access.Read,
                    ActionEnum.Create => access.Create,
                    ActionEnum.Delete => access.Delete,
                    ActionEnum.Update => access.Update,
                    _ => false,
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

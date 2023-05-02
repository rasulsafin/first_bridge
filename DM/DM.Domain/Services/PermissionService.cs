using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Interfaces;

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
            var permissions = await Context.Permissions.GetAllByRole(roleId);
            return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }

        public async Task<bool> UpdatePermissionOnRole(PermissionDto permissionModel)
        {
            var permission = await Context.Permissions.GetAllByRoleAndType(permissionModel.RoleId, permissionModel.Type);

            if (permission == null) return false;

            permission.Type = permissionModel.Type;
            permission.Create = permissionModel.Create;
            permission.Read = permissionModel.Read;
            permission.Update = permissionModel.Update;
            permission.Delete = permissionModel.Delete;
            permission.UpdatedAt = DateTime.UtcNow;

            Context.Permissions.Update(permission);
            await Context.SaveAsync();

            return true;
        }
    }
}

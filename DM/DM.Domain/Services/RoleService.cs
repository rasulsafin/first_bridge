using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL;
using DM.DAL.Entities;
using System;
using DM.DAL.Interfaces;

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

        public async Task<bool> Create(RoleForCreateDto roleForCreateModel)
        {
            var role = _mapper.Map<Role>(new RoleDto
            {
                Name = roleForCreateModel.Name,
                Description = roleForCreateModel.Description,
                Permissions = roleForCreateModel.Permissions,
            });

            await Context.Roles.Create(role);
            await Context.SaveAsync();

            return true;
        }
        public async Task<bool> Update(RoleForUpdateDto roleForUpdateModel)
        {
            var role = Context.Roles.GetById(roleForUpdateModel.Id);

            if (role == null) return false;

            role.Name = roleForUpdateModel.Name;
            role.Description = roleForUpdateModel.Description;
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

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

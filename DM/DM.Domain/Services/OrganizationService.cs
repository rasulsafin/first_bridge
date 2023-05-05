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
    public class OrganizationService : IOrganizationService
    {
        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;

        public OrganizationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Context = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrganizationDto>> GetAll()
        {
            var organizations = await Context.Organizations.GetAll();
            return _mapper.Map<IEnumerable<OrganizationDto>>(organizations);
        }

        public OrganizationDto GetById(long organizationId)
        {
            if (organizationId < 1) return null;

            var organization = Context.Organizations.GetById(organizationId);
            return _mapper.Map<OrganizationDto>(organization);
        }

        public async Task<bool> Create(OrganizationForCreateDto organizationForCreateDto)
        {
            var organization = _mapper.Map<Organization>(new OrganizationForCreateDto
            {
                Inn = organizationForCreateDto.Inn,
                Kpp = organizationForCreateDto.Kpp,
                Name = organizationForCreateDto.Name,
                Ogrn = organizationForCreateDto.Ogrn,
                Phone = organizationForCreateDto.Phone,
                Email = organizationForCreateDto.Email,
                Address = organizationForCreateDto.Address
            });

            await Context.Organizations.Create(organization);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> Update(OrganizationForUpdateDto organizationForUpdateDto)
        {
            var organization = Context.Organizations.GetById(organizationForUpdateDto.Id);

            if (organization == null) return false;

            organization.Name = organizationForUpdateDto.Name;
            organization.Address = organizationForUpdateDto.Address;
            organization.Inn = organizationForUpdateDto.Inn;
            organization.Ogrn = organizationForUpdateDto.Ogrn;
            organization.Kpp = organizationForUpdateDto.Kpp;
            organization.Phone = organizationForUpdateDto.Phone;
            organization.Email = organizationForUpdateDto.Email;
            organization.UpdatedAt = DateTime.UtcNow;

            Context.Organizations.Update(organization);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(long organizationId)
        {
            var result = Context.Organizations.Delete(organizationId);
            await Context.SaveAsync();

            return result;
        }

        public async Task<PermissionDto> GetAccess(long roleId)
        {
            var access = await Context.Permissions.GetByRoleAndType(roleId, PermissionEnum.Organization);
            return _mapper.Map<PermissionDto>(access);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

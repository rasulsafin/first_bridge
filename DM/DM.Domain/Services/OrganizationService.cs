using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL.Interfaces;

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

        public async Task<bool> Create(OrganizationForCreateDto organizationForCreateModel)
        {
            var organization = _mapper.Map<Organization>(new OrganizationForCreateDto
            {
                Inn = organizationForCreateModel.Inn,
                Kpp = organizationForCreateModel.Kpp,
                Name = organizationForCreateModel.Name,
                Ogrn = organizationForCreateModel.Ogrn,
                Phone = organizationForCreateModel.Phone,
                Email = organizationForCreateModel.Email,
                Address = organizationForCreateModel.Address
            });

            await Context.Organizations.Create(organization);
            await Context.SaveAsync();

            return true;
        }

        public async Task<bool> Update(OrganizationForUpdateDto organizationForUpdateModel)
        {
            var organization = Context.Organizations.GetById(organizationForUpdateModel.Id);

            if (organization == null) return false;

            organization.Name = organizationForUpdateModel.Name;
            organization.Address = organizationForUpdateModel.Address;
            organization.Inn = organizationForUpdateModel.Inn;
            organization.Ogrn = organizationForUpdateModel.Ogrn;
            organization.Kpp = organizationForUpdateModel.Kpp;
            organization.Phone = organizationForUpdateModel.Phone;
            organization.Email = organizationForUpdateModel.Email;
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
    }
}

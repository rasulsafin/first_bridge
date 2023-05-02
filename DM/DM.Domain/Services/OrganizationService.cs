using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL;
using System;

namespace DM.Domain.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly DmDbContext _context;

        private readonly IMapper _mapper;

        public OrganizationService(DmDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrganizationDto>> GetAll()
        {
            var organizations = await _context.Organization
                .Include(x => x.Users)
                .Include(y => y.Projects).ToListAsync();

            return _mapper.Map<List<OrganizationDto>>(organizations);
        }

        public async Task<OrganizationDto> GetById(long organizationId)
        {
            var organization = await _context.Organization
                .Include(x => x.Users)
                .Include(y => y.Projects)
                .FirstOrDefaultAsync(z => z.Id == organizationId);

            return _mapper.Map<OrganizationDto>(organization);
        }

        public async Task<bool> Create(OrganizationForCreateDto organizationForCreateModel)
        {
            var organization = new Organization()
            {
                Inn = organizationForCreateModel.Inn,
                Kpp = organizationForCreateModel.Kpp,
                Name = organizationForCreateModel.Name,
                Ogrn = organizationForCreateModel.Ogrn,
                Phone = organizationForCreateModel.Phone,
                Email = organizationForCreateModel.Email,
                Address = organizationForCreateModel.Address
            };

            _context.Organization.Add(organization);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(OrganizationForUpdateDto organizationForUpdateModel)
        {
            var organization = await _context.Organization
                .Where(q => q.Id == organizationForUpdateModel.Id).FirstOrDefaultAsync();

            if (organization == null) return false;

            _context.Organization.Attach(organization);

            organization.Name = organizationForUpdateModel.Name;
            organization.Address = organizationForUpdateModel.Address;
            organization.Inn = organizationForUpdateModel.Inn;
            organization.Ogrn = organizationForUpdateModel.Ogrn;
            organization.Kpp = organizationForUpdateModel.Kpp;
            organization.Phone = organizationForUpdateModel.Phone;
            organization.Email = organizationForUpdateModel.Email;
            organization.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _context.Entry(organization).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> Delete(long organizationId)
        {
            var organization = _context.Organization
                .Include(x => x.Projects).Include(x => x.Users)
                .FirstOrDefault(q => q.Id == organizationId);

            if (organization == null) return false;

            _context.Organization.Remove(organization);

            await _context.SaveChangesAsync();

            return true;
        }


    }
}

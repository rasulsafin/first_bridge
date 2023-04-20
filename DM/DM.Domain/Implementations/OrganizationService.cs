using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using DM.Domain.Interfaces;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL;

namespace DM.Domain.Implementations
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

        public async Task<List<OrganizationModel>> GetAll()
        {
            var organizations = await _context.Organization
                .Include(x => x.Users)
                .Include(y => y.Projects).ToListAsync();

            return _mapper.Map<List<OrganizationModel>>(organizations);
        }

        public async Task<OrganizationModel> GetById(long organizationId)
        {
            var organization = await _context.Organization
                .Include(x => x.Users)
                .Include(y => y.Projects)
                .FirstOrDefaultAsync(z => z.Id == organizationId);

            return _mapper.Map<OrganizationModel>(organization);
        }

        public async Task<bool> Create(OrganizationForCreateModel organizationModel)
        {
            var organization = new OrganizationEntity()
            {
                Inn = organizationModel.Inn,
                Kpp = organizationModel.Kpp,
                Name = organizationModel.Name,
                Ogrn = organizationModel.Ogrn,
                Phone = organizationModel.Phone,
                Email = organizationModel.Email,
                Address = organizationModel.Address
            };

            _context.Organization.Add(organization);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(OrganizationForUpdateModel organizationModel)
        {
            var fieldForUpdate = await _context.Organization
                .Where(q => q.Id == organizationModel.Id).FirstOrDefaultAsync();

            if (fieldForUpdate == null) return false;

            _context.Organization.Attach(fieldForUpdate);

            fieldForUpdate.Name = organizationModel.Name;
            fieldForUpdate.Address = organizationModel.Address;
            fieldForUpdate.Inn = organizationModel.Inn;
            fieldForUpdate.Ogrn = organizationModel.Ogrn;
            fieldForUpdate.Kpp = organizationModel.Kpp;
            fieldForUpdate.Phone = organizationModel.Phone;
            fieldForUpdate.Email = organizationModel.Email;

            await _context.SaveChangesAsync();

            _context.Entry(fieldForUpdate).State = EntityState.Detached;

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

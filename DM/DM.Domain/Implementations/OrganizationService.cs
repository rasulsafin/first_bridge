using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DM.Domain.Implementations
{
    public class OrganizationService : IOrganizationService
    {
        private readonly DmDbContext _context;

        public OrganizationService(DmDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrganizationEntity>> GetAll()
        {
            var result = await _context.Organization.Include(x => x.Users).Include(y => y.Projects).ToListAsync();
            return result;
        }

        public async Task<OrganizationEntity> GetById(long organizationId)
        {
            var result = await _context.Organization.Include(x => x.Users).Include(y => y.Projects).FirstOrDefaultAsync(z => z.Id == organizationId);
            return result;
        }

        public async Task<bool> Create(OrganizationModelForCreate organizationModel)
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

            
            await _context.Organization.AddAsync(organization);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> Update(OrganizationModelForUpdate organizationModel)
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
            //TODO: check that the Records/fields do not contain users to be deleted

            var organization = _context.Organization
                .Include(x => x.Projects).Include(x => x.Users)
                .FirstOrDefault(q => q.Id == organizationId);

            if (organization == null)
            {
                return false;
            }
            
            _context.Organization.Remove(organization);
            await _context.SaveChangesAsync();
            
            return true;
        }
        
        
    }
}

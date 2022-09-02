using AutoMapper;
using DM.DAL.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            
            var result = await _context.Organization.AddAsync(organization);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}

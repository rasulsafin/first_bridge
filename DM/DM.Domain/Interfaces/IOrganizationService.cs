using DM.DAL.Entities;
using DM.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IOrganizationService
    {
        public Task<List<OrganizationEntity>> GetAll();
        public Task<OrganizationEntity> GetById(long organizationId);
        public Task<bool> Create(OrganizationModel organizationModel);
    }
}

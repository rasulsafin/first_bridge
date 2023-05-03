using System.Collections.Generic;
using System.Threading.Tasks;

using DM.DAL.Entities;
using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IOrganizationService
    {
        public Task<IEnumerable<OrganizationDto>> GetAll();
        public OrganizationDto GetById(long organizationId);
        public Task<bool> Create(OrganizationForCreateDto organizationModel);
        public Task<bool> Update(OrganizationForUpdateDto organizationModel);
        public Task<bool> Delete(long organizationId);
        void Dispose();
    }
}

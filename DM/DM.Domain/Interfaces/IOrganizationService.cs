using System.Collections.Generic;
using System.Threading.Tasks;

using DM.DAL.Entities;
using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface IOrganizationService : IGetAccess
    {
        public Task<IEnumerable<OrganizationDto>> GetAll();
        public OrganizationDto GetById(long organizationId);
        public Task<bool> Create(OrganizationForCreateDto organization);
        public Task<bool> Update(OrganizationForUpdateDto organization);
        public Task<bool> Delete(long organizationId);
        void Dispose();
    }
}

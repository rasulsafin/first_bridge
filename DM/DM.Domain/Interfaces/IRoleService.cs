using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface IRoleService : IGetAccess
    {
        public Task<IEnumerable<RoleDto>> GetAll();
        public RoleDto GetById(long roleId);
        public Task<bool> Create(RoleForCreateDto role);
        public Task<bool> Update(RoleForUpdateDto role);
        public Task<bool> Delete(long role);
        void Dispose();
    }
}

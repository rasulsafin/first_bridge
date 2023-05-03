using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IRoleService
    {
        public Task<IEnumerable<RoleDto>> GetAll();
        public RoleDto GetById(long roleId);
        public Task<bool> Create(RoleForCreateDto roleModel);
        public Task<bool> Update(RoleForUpdateDto roleModel);
        public Task<bool> Delete(long roleModel);
    }
}

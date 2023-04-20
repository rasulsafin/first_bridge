using System.Collections.Generic;
using System.Threading.Tasks;

using DM.Domain.Models;

namespace DM.Domain.Interfaces
{
    public interface IRoleService
    {
        public List<RoleModel> GetAll();
        public RoleModel GetById(long roleId);
        public Task<bool> Create(RoleForCreateModel roleModel);
        public Task<bool> Update(RoleForUpdateModel roleModel);
        public Task<bool> Delete(long roleModel);
    }
}

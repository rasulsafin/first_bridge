using DM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IRoleService
    {
        public List<RoleModel> GetAll();
        public RoleModel GetById(long roleId);
        public Task<bool> Create(RoleModel roleModel);
        public Task<bool> Update(RoleModel roleModel);
        public Task<bool> Delete(long roleModel);
    }
}

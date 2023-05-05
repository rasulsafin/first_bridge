using DM.DAL.Entities;
using DM.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IPermissionService : IGetAccess
    {
        public Task<IEnumerable<PermissionDto>> GetAllByRole(long roleId);
        public Task<bool> UpdatePermissionOnRole(PermissionDto permission);
        void Dispose();
    }
}

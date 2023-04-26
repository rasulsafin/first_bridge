using DM.DAL.Entities;
using DM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Interfaces
{
    public interface IPermissionService
    {
        public Task<List<PermissionModel>> GetAllByRole(long roleId);
        public Task<bool> UpdatePermissionOnRole(PermissionModel permissionModel);
    }
}

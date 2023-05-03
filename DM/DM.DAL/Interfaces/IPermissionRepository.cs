using DM.DAL.Entities;
using DM.DAL.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DM.DAL.Interfaces
{
    public interface IPermissionRepository<T> : IRepository<Permission>
    {
        Task<IEnumerable<T>> GetAllByRole(long roleId);
        Task<T> GetByRoleAndType(long roleId, PermissionEnum permission);
    }
}

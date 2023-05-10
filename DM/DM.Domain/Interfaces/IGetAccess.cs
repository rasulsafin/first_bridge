using System.Threading.Tasks;

using DM.Domain.DTO;

using DM.Common.Enums;

namespace DM.Domain.Interfaces
{
    public interface IGetAccess
    {
        Task<bool> GetAccess(long roleId, ActionEnum action);
    }
}

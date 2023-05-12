using System;
using System.Threading.Tasks;

using DM.Common.Enums;

namespace DM.Domain.Interfaces
{
    public interface IGetAccess
    {
        public Task<bool> GetAccess(long roleId, ActionEnum action);
    }
}

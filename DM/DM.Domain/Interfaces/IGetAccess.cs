﻿using System.Threading.Tasks;

using DM.Domain.DTO;

namespace DM.Domain.Interfaces
{
    public interface IGetAccess
    {
        Task<PermissionDto> GetAccess(long roleId);
    }
}

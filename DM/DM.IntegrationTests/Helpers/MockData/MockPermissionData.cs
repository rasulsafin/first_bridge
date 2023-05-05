using DM.Common.Enums;
using DM.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.IntegrationTests.Helpers.MockData
{
    public class MockPermissionData
    {
        private static readonly List<PermissionDto> permissions = new()
        {
            new PermissionDto { Id=1, RoleId = 1, Type = PermissionEnum.Project, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
            new PermissionDto { Id=2, RoleId = 1, Type = PermissionEnum.Role, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
            new PermissionDto { Id=3, RoleId = 1, Type = PermissionEnum.Organization, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
            new PermissionDto { Id=4, RoleId = 1, Type = PermissionEnum.Template, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
            new PermissionDto { Id=5, RoleId = 1, Type = PermissionEnum.Record, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
            new PermissionDto { Id=6, RoleId = 1, Type = PermissionEnum.Item, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
            new PermissionDto { Id=7, RoleId = 1, Type = PermissionEnum.User, Create = true, Read = true, Update = true, Delete = true, CreatedAt = DateTime.Today},
        };
    }
}

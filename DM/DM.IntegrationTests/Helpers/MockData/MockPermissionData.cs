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
        public static PermissionDto PERMISSION_WITH_ALL_ACCESS_DTO= new()
        {
            //TODO real id,
            Create = true,
            Read = true,
            Update = true,
            Delete = true,
            CreatedAt = DateTime.Now,
        };
    }
}

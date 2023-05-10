using DM.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.IntegrationTests.Helpers.MockData
{
    public class MockRoleData
    {
        public static RoleForCreateDto ROLE_FOR_CREATE = new()
        {
            Name = "Test Role",
            Description = "Description about Test Role",
            //TODO add permissions
            CreatedAt = DateTime.Now,
        };

        public static RoleForUpdateDto ROLE_FOR_UPDATE = new()
        {
            Name = "Test Role",
            Description = "Description about Test Role",
            CreatedAt = DateTime.Now,
        };
    }
}

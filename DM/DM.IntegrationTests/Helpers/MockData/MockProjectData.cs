using DM.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.IntegrationTests.Helpers.MockData
{
    public class MockProjectData
    {
        public static ProjectForReadDto PROJECT_FOR_READ = new()
        {
            Title = "Project - 1",
            IsInArchive = false,
            OrganizationId = 1,//TODO real id,
                               //TODO add Items
            CreatedAt = DateTime.Now,
        };

        public static ProjectForUpdateDto PROJECT_FOR_UPDATE = new()
        {
            Title = "Project - 1",
            IsInArchive = false,
            OrganizationId = 1,//TODO real id,
                               //TODO add Items
            CreatedAt = DateTime.Now,
        };
    }
}

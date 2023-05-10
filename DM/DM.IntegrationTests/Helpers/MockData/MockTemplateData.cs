using DM.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.IntegrationTests.Helpers.MockData
{
    public class MockTemplateData
    {
        public static TemplateForCreateDto TEMPLATE_FOR_CREATE = new()
        {
            Name = "Template - 1",
            ProjectId = 1,//TODO real id,
                          //TODO add fields
            CreatedAt = DateTime.Now,
        };

        public static TemplateForUpdateDto TEMPLATE_FOR_UPDATE = new()
        {
            Name = "Template - 2",
            ProjectId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };
    }
}

using DM.Common.Enums;
using DM.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.IntegrationTests.Helpers.MockData
{
    public class MockRecordData
    {
        public static RecordForCreateDto RECORD_FOR_CREATE = new()
        {
            Name = "Record - 1",
            Description = "Description about Record - 1",
            Executor = "User For Create",
            IsInArchive = false,
            FixDate = DateTime.Now.AddMonths(1),
            Status = StatusEnum.Open,
            Priority = PriorityEnum.Medium,
            TemplateId = 1,//TODO real id,
            CreatedAt = DateTime.Now,
        };

        public static RecordForReadDto RECORD_FOR_READ = new()
        {
            Name = "Record - 2",
            Description = "Description about Record - 2",
            Executor = "User For Read",
            IsInArchive = false,
            FixDate = DateTime.Now.AddMonths(2),
            Status = StatusEnum.Moved,
            Priority = PriorityEnum.High,
            TemplateId = 2,//TODO real id,
                           //TODO Comments
            CreatedAt = DateTime.Now,
        };
    }
}

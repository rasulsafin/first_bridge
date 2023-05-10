using System;
using System.Collections.Generic;

using DM.Common.Enums;

namespace DM.Domain.DTO
{
    public class RecordDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //Исполнитель
        public string Executor { get; set; }
        public bool IsInArchive { get; set; }

        //Дата устранения
        public DateTime FixDate { get; set; }

        public StatusEnum Status { get; set; }
        public PriorityEnum Priority { get; set; }

        public long ProjectId { get; set; }
        public long? TemplateId { get; set; }

        public List<int> FieldIds { get; set; }
        public List<FieldDto> Fields { get; set; }

        public List<int> ListFieldIds { get; set; }
        public List<ListFieldDto> ListFields { get; set; }
    }
}

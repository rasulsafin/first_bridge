using System.Collections.Generic;

using DM.Common.Enums;

namespace DM.Domain.DTO
{
    public class ListFieldDto : BaseDto
    {
        public string Name { get; set; }
        public bool IsMandatory { get; set; }

        public FieldEnum Type { get; set; }

        public long? RecordId { get; set; }
        public long? TemplateId { get; set; }

        public List<int> ListIds { get; set; }
        public List<ListDto> Lists { get; set; }

        public ListFieldDto()
        {
            Type = FieldEnum.List;
            IsMandatory = false;
        }
    }
}

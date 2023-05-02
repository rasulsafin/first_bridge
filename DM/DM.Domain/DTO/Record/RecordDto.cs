using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class RecordDto : BaseDto
    {
        public string Name { get; set; }

        public long ProjectId { get; set; }

        public List<int> FieldIds { get; set; }
        public List<FieldDto> Fields { get; set; }

        public List<int> ListFieldIds { get; set; }
        public List<ListFieldDto> ListFields { get; set; }
    }
}

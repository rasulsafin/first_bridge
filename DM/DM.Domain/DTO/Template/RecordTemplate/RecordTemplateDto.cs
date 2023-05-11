using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class RecordTemplateDto
    {
        public ICollection<int> RecordIds { get; set; }
        public ICollection<RecordDto> Records { get; set; }
    }
}

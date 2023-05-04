using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class RecordTemplateDto
    {
        public List<int> RecordIds { get; set; }
        public List<RecordDto> Records { get; set; }
    }
}

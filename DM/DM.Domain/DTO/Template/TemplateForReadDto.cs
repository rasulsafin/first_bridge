using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class TemplateForReadDto : TemplateDto
    {
        public List<int> FieldIds { get; set; }
        public List<FieldDto> Fields { get; set; }

        public List<int> ListFieldIds { get; set; }
        public List<ListFieldDto> ListFields { get; set; }
    }
}

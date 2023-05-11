using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class TemplateForReadDto : TemplateDto
    {
        public ICollection<int> FieldIds { get; set; }
        public ICollection<FieldDto> Fields { get; set; }

        public ICollection<int> ListFieldIds { get; set; }
        public ICollection<ListFieldDto> ListFields { get; set; }
    }
}

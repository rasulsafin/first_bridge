using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class DocumentTemplateDto : TemplateDto
    {
        public ICollection<int> DocumentIds { get; set; }
        public ICollection<DocumentDto> Documents { get; set; }
    }
}

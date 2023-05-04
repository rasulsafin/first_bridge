using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class DocumentTemplateDto : TemplateDto
    {
        public List<int> DocumentIds { get; set; }
        public List<DocumentDto> Documents { get; set; }
    }
}

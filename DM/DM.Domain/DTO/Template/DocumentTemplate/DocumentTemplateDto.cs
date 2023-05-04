using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class DocumentTemplateDto : TemplateDto
    {
        public List<int> DocumentIds { get; set; }
        public List<DocumentDto> Documents { get; set; }
    }
}

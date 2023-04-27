using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class DocumentTemplateModel : TemplateModel
    {
        public List<int> DocumentIds { get; set; }
        public List<DocumentModel> Documents { get; set; }
    }
}

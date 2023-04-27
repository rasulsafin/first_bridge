using System.Collections.Generic;

namespace DM.DAL.Entities
{
    public class DocumentTemplateEntity : TemplateEntity
    {
        public List<DocumentEntity> Documents { get; set; }
    }
}

using System.Collections.Generic;

namespace DM.DAL.Entities
{
    public class DocumentTemplate : Template
    {
        public ICollection<Document> Documents { get; set; }
    }
}

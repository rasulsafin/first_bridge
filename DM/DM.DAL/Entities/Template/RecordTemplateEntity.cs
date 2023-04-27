using System.Collections.Generic;

namespace DM.DAL.Entities
{
    public class RecordTemplateEntity : TemplateEntity
    {
        public List<RecordEntity> Records { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Entities
{
    public class TemplateListEntity : BaseEntity
    {
        public long TemplateId { get; set; }
        public TemplateEntity Template { get; set; }

        public long ListId { get; set; }
        public ListFieldEntity ListField { get; set; }
    }
}

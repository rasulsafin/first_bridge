using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Entities
{
    public class TemplateFieldEntity : BaseEntity
    {
        public long TemplateId { get; set; }
        public TemplateEntity Template { get; set; }

        public long FieldId { get; set; }
        public FieldEntity Field { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Entities
{
    public class RecordFieldEntity : BaseEntity
    {
        public long RecordId { get; set; }
        public RecordEntity Record { get; set; }

        public long FieldId { get; set; }
        public FieldEntity Field { get; set; }
    }
}

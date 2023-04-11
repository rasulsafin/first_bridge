using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Entities
{
    public class RecordListEntity : BaseEntity
    {
        public long RecordId { get; set; }
        public RecordEntity Record { get; set; }

        public long ListId { get; set; }
        public ListFieldEntity ListField { get; set; }
    }
}

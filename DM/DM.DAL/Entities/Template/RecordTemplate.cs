using System.Collections.Generic;

namespace DM.DAL.Entities
{
    public class RecordTemplate : Template
    {
        public ICollection<Record> Records { get; set; }
    }
}

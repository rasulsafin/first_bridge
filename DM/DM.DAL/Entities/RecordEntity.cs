using DM.Entities;
using System.Collections.Generic;

namespace DM.DAL.Entities
{
    public class RecordEntity : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<FieldsEntity> Fields { get; set; }
    }
}

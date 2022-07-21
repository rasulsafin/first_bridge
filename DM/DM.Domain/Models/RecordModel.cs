using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class RecordModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public List<FieldsModel> Fields { get; set; }
    }
}

using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class RecordModel : RecordModelForCreate
    {
        public int Id { get; set; }
    }

    public class RecordModelForCreate
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public List<FieldsModelForCreate> Fields { get; set; }
    }
}

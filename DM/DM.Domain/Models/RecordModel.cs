using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class RecordModel : RecordModelBase
    {
        public long Id { get; set; }
        public List<FieldsModel> Fields { get; set; }
    }

    public class RecordModelForCreate : RecordModelBase
    {
        public List<FieldsModelForCreate> Fields { get; set; }
    }

    public class RecordModelBase
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
    }
}

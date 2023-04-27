using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class RecordModel : BaseModel
    {
        public string Name { get; set; }

        public long ProjectId { get; set; }

        public List<int> FieldIds { get; set; }
        public List<FieldModel> Fields { get; set; }

        public List<int> ListFieldIds { get; set; }
        public List<ListFieldModel> ListFields { get; set; }
    }
}

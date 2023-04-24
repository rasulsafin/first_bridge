using System.Collections.Generic;

using DM.DAL.Enums;

namespace DM.Domain.Models
{
    public class ListFieldModel : BaseModel
    {
        public string Name { get; set; }
        public FieldType Type { get; set; } = FieldType.List;
        public bool IsMandatory { get; set; }
        public List<ListModel> Lists { get; set; }
        public long? RecordId { get; set; }
        public long? TemplateId { get; set; }
    }
}

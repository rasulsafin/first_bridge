using DM.DAL.Enums;

namespace DM.Domain.Models
{
    public class FieldModel : BaseModel
    {
        public string Name { get; set; }
        public FieldType Type { get; set; } = FieldType.Text;
        public bool IsMandatory { get; set; }
        public string Data { get; set; }
        public long? RecordId { get; set; }
        public long? TemplateId { get; set; }
    }
}

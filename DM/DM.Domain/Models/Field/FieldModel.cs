using DM.DAL.Enums;

namespace DM.Domain.Models
{
    public class FieldModel : BaseModel
    {
        public string Name { get; set; }
        public bool IsMandatory { get; set; }
        public string Data { get; set; }

        public FieldEnum Type { get; set; }

        public long? RecordId { get; set; }
        public long? TemplateId { get; set; }

        public FieldModel()
        {
            Type = FieldEnum.Text;
            IsMandatory = false;
        }
    }
}

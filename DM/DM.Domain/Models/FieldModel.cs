using DM.DAL.Entities;

namespace DM.Domain.Models
{
    public class FieldModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public bool IsMandatory { get; set; }
    }
}

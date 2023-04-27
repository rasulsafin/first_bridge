using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using DM.DAL.Enums;

namespace DM.DAL.Entities
{
    [Table("Field")]
    public class FieldEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Data { get; set; }
        public bool IsMandatory { get; set; }

        public FieldEnum Type { get; set; }

        public long? TemplateId { get; set; }
        public TemplateEntity Template { get; set; }

        public long? RecordId { get; set; }
        public RecordEntity Record { get; set; }
    }
}

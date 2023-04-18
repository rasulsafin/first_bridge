using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    [Table("Field")]
    public class FieldEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public FieldType Type { get; set; } = FieldType.Text;
        public bool IsMandatory { get; set; } = false;
        public string Data { get; set; }

        public long? TemplateId { get; set; }
        public TemplateEntity Template { get; set; }

        public long? RecordId { get; set; }
        public RecordEntity Record { get; set; }
    }

    [Table("ListField")]
    public class ListFieldEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public FieldType Type { get; set; } = FieldType.List;
        public bool IsMandatory { get; set; } = false;

        public List<ListEntity> Lists { get; set; }

        public long? TemplateId { get; set; }
        public TemplateEntity Template { get; set; }

        public long? RecordId { get; set; }
        public RecordEntity Record { get; set; }
    }

    public class ListEntity : BaseEntity
    {
        public long ListId { get; set; }
        public ListFieldEntity List { get; set; }
        public string Data { get; set; }
    }

    public enum FieldType
    {
        Text = 1, List = 2
    }
}

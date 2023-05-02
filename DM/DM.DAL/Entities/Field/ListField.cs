using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using DM.DAL.Enums;

namespace DM.DAL.Entities
{
    [Table("ListField")]
    public class ListField : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public bool IsMandatory { get; set; }

        public FieldEnum Type { get; set; }

        public long? TemplateId { get; set; }
        public Template Template { get; set; }

        public long? RecordId { get; set; }
        public Record Record { get; set; }

        public ICollection<List> Lists { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DM.DAL.Entities
{
    public class TemplateEntity : BaseEntity
    {
        public string Name { get; set; }

        [Required]
        public long ProjectId { get; set; }
        public ProjectEntity Project { get; set; }

        public List<FieldEntity> Fields { get; set; }
        public List<ListFieldEntity> ListFields { get; set; }
    }
}

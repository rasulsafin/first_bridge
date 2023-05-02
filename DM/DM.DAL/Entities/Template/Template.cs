using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DM.DAL.Entities
{
    public class Template : BaseEntity
    {
        public string Name { get; set; }

        [Required]
        public long ProjectId { get; set; }
        public Project Project { get; set; }

        public ICollection<Field> Fields { get; set; }
        public ICollection<ListField> ListFields { get; set; }
    }
}

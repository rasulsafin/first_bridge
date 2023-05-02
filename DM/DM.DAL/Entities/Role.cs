using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    [Table("Role")]
    public class Role : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
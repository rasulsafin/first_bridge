using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    [Table("Role")]
    public class RoleEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public List<int> PermissionIds { get; set; }
        public List<PermissionEntity> Permissions { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DM.DAL.Enums;

namespace DM.DAL.Entities
{
    [Table("Permission")]
    public class PermissionEntity : BaseEntity
    {
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }

        public PermissionEnum Type { get; set; }

        [Required]
        public long RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
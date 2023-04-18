using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    [Table("Permission")]
    public class PermissionEntity : BaseEntity
    {
        [Required]
        public long RoleId { get; set; }
        public RoleEntity Role { get; set; }
        public PermissionType Type { get; set; }
        public bool Create { get; set; } = false;
        public bool Read { get; set; } = false;
        public bool Update { get; set; } = false;
        public bool Delete { get; set; } = false;
    }

    public enum PermissionType
    {
        Project = 1, Item = 2, Record = 3, Template = 4, Role = 5, User = 6, Organization = 7
    }
}
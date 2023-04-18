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
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }

    public enum PermissionType
    {
        Project = 1, Item = 2, Record = 3, Template = 4, Role = 5, User = 6, Organization = 7
    }
}
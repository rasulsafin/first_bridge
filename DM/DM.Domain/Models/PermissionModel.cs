using DM.DAL.Enums;

namespace DM.Domain.Models
{
    public class PermissionModel : BaseModel
    {
        public long RoleId { get; set; }
        public PermissionType Type { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}

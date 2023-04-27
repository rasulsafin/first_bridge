using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class RoleModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<int> UserIds { get; set; }
        public List<UserModel> Users { get; set; }

        public List<int> PermissionIds { get; set; }
        public List<PermissionModel> Permissions { get; set; }
    }
}

using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<UserModel> Users { get; set; }
        public List<PermissionModel> Permissions { get; set; }
    }
}

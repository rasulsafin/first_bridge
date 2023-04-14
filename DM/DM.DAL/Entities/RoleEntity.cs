using System.Collections.Generic;

namespace DM.DAL.Entities
{
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public List<UserEntity> Users { get; set; }
        public List<PermissionEntity> Permissions { get; set; }
    }
}
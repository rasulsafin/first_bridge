using System.Collections.Generic;

namespace WrapperDM.Entities
{
    public class RoleEntity : BaseEntity
    {
        public string? Name { get; set; }
        public List<UserEntity>? Users { get; set; }
    }
}
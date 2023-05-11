using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class RoleDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<int> UserIds { get; set; }
        public ICollection<UserDto> Users { get; set; }

        public ICollection<int> PermissionIds { get; set; }
        public ICollection<PermissionDto> Permissions { get; set; }
    }
}

using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class RoleDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<int> UserIds { get; set; }
        public List<UserDto> Users { get; set; }

        public List<int> PermissionIds { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}

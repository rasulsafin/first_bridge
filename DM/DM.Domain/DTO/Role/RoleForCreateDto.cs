using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class RoleForCreateDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<int> PermissionIds { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}

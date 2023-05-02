using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Models
{
    public class RoleForCreateDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<int> PermissionIds { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}

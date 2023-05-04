using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.DTO
{
    public class OrganizationForUpdateDto : OrganizationDto
    {
        public List<int> UserIds { get; set; }
        public List<UserDto> Users { get; set; }

        public List<int> ProjectIds { get; set; }
        public List<ProjectDto> Projects { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.DTO
{
    public class OrganizationForUpdateDto : OrganizationDto
    {
        public ICollection<int> UserIds { get; set; }
        public ICollection<UserDto> Users { get; set; }

        public ICollection<int> ProjectIds { get; set; }
        public ICollection<ProjectDto> Projects { get; set; }
    }
}

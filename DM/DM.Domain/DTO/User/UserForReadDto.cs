using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class UserForReadDto : UserDto
    {
        public ICollection<int> ProjectsIds { get; set; }
        public ICollection<ProjectDto> Projects { get; set; }
    }
}

using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class UserForReadDto : UserDto
    {
        public List<int> ProjectsIds { get; set; }
        public List<ProjectDto> Projects { get; set; }
    }
}

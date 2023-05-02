using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class ProjectForReadDto : ProjectDto
    {
        public List<int> ItemIds { get; set; }
        public List<ItemDto> Items { get; set; }

        public List<int> UserIds { get; set; }
        public List<UserDto> Users { get; set; }
    }
}

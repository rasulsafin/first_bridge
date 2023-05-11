using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class ProjectForReadDto : ProjectDto
    {
        public ICollection<int> ItemIds { get; set; }
        public ICollection<ItemDto> Items { get; set; }

        public ICollection<int> UserIds { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}

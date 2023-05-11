using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class RecordForReadDto : RecordDto
    {
        public ICollection<int> CommentIds { get; set; }
        public ICollection<CommentForReadDto> Comments { get; set; }
    }
}

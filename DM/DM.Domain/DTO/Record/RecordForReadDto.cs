using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class RecordForReadDto : RecordDto
    {
        public List<int> CommentIds { get; set; }
        public List<CommentForReadDto> Comments { get; set; }
    }
}

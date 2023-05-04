using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class RecordForReadDto : RecordDto
    {
        public List<int> CommentIds { get; set; }
        public List<CommentForReadDto> Comments { get; set; }
    }
}

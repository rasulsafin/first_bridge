using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class RecordForReadModel : RecordModel
    {
        public List<int> CommentIds { get; set; }
        public List<CommentForReadModel> Comments { get; set; }
    }
}

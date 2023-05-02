namespace DM.Domain.Models
{
    public class CommentDto : BaseDto
    {
        public string Text { get; set; }

        public long RecordId { get; set; }
        public long UserId { get; set; }
    }
}
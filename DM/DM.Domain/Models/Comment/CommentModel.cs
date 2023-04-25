namespace DM.Domain.Models
{
    public class CommentModel : BaseModel
    {
        public long RecordId { get; set; }
        public long UserId { get; set; }
        public string Text { get; set; }
    }
}
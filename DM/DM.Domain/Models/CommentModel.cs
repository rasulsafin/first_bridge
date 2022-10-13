namespace DM.Domain.Models
{
    public class CommentModel : CommentModelForGet
    {
        public long RecordId { get; set; }
    }

    public class CommentModelForUpdate
    {
        public long CommentId { get; set; }
        public string Text { get; set; }
    }

    public class CommentModelForGet
    {
        public string UserName { get; set; }
        public string Text { get; set; }
    }
}
namespace DM.Domain.Models
{
    public class UserProjectModel : BaseModel
    {
        public long UserId { get; set; }
        public long ProjectId { get; set; }
    }
}

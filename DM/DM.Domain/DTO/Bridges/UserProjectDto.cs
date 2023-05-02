namespace DM.Domain.Models
{
    public class UserProjectDto : BaseDto
    {
        public long UserId { get; set; }
        public long ProjectId { get; set; }
    }
}

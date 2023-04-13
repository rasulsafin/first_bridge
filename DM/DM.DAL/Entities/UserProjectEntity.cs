namespace DM.DAL.Entities
{
    /// <summary>
    /// many to many
    /// </summary>
    public class UserProjectEntity : BaseEntity
    {
        public long UserId { get; set; }
        public UserEntity User { get; set; }

        public long ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
    }
}

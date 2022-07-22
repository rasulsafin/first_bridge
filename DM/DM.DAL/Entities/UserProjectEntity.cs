using DM.Entities;

namespace DM.DAL.Entities
{
    /// <summary>
    /// many to many
    /// </summary>
    public class UserProjectEntity : BaseEntity
    {
        public int UserId { get; set; }

        public UserEntity User { get; set; }

        public int ProjectId { get; set; }

        public ProjectEntity Project { get; set; }
    }
}

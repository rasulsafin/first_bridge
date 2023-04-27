using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    /// <summary>
    /// many to many
    /// </summary>
    [Table("UserProject")]
    public class UserProjectEntity : BaseEntity
    {
        [Required]
        public long UserId { get; set; }
        public UserEntity User { get; set; }

        [Required]
        public long ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
    }
}

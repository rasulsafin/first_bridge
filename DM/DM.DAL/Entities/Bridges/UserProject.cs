using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    /// <summary>
    /// many to many
    /// </summary>
    [Table("UserProject")]
    public class UserProject : BaseEntity
    {
        [Required]
        public long UserId { get; set; }
        public User User { get; set; }

        [Required]
        public long ProjectId { get; set; }
        public Project Project { get; set; }
    }
}

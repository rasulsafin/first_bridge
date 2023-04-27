using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    [Table("Comment")]
    public class CommentEntity : BaseEntity
    {
        public string Text { get; set; }

        [Required]
        public long UserId { get; set; }
        public UserEntity User { get; set; }

        [Required]
        public long RecordId { get; set; }
        public RecordEntity Record { get; set; }
    }
}
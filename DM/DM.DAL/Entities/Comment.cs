using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    [Table("Comment")]
    public class Comment : BaseEntity
    {
        public string Text { get; set; }

        [Required]
        public long UserId { get; set; }
        public User User { get; set; }

        [Required]
        public long RecordId { get; set; }
        public Record Record { get; set; }
    }
}
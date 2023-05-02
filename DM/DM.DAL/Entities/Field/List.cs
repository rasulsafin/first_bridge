using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    [Table("List")]
    public class List : BaseEntity
    {
        public string Data { get; set; }

        [Required]
        public long ListId { get; set; }
        public ListField ListField { get; set; }
    }
}

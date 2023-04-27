using System.ComponentModel.DataAnnotations;

namespace DM.DAL.Entities
{
    public class ListEntity : BaseEntity
    {
        public string Data { get; set; }

        [Required]
        public long ListId { get; set; }
        public ListFieldEntity List { get; set; }
    }
}

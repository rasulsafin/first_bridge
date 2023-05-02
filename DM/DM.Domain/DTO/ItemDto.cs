namespace DM.Domain.Models
{
    public class ItemDto : BaseDto
    {
        public string Name { get; set; }
        public string RelativePath { get; set; }

        public long ProjectId { get; set; }
    }
}

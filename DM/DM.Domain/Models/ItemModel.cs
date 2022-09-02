namespace DM.Domain.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RelativePath { get; set; }
        public long ProjectId { get; set; }
    }
}

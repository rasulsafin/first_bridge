namespace DM.Domain.Models
{
    public class ItemModel
    {
        public string Name { get; set; }
        public string RelativePath { get; set; }
        public ProjectModel Project { get; set; }
    }
}

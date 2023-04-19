namespace DM.Domain.Models
{
    public class ProjectForUpdateModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsInArchive { get; set; }
    }
}

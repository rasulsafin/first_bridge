namespace DM.Domain.Models
{
    public class ProjectModel : BaseModel
    {
        public string Title { get; set; }
        public bool IsInArchive { get; set; }
        public long OrganizationId { get; set; }

        public ProjectModel()
        {
            IsInArchive = false;
        }
    }
}

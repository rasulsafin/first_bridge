namespace DM.Domain.DTO
{
    public class ProjectDto : BaseDto
    {
        public string Title { get; set; }
        public bool IsInArchive { get; set; }

        public long OrganizationId { get; set; }

        public ProjectDto()
        {
            IsInArchive = false;
        }
    }
}

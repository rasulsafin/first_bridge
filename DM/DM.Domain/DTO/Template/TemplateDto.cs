namespace DM.Domain.Models
{
    public class TemplateDto : BaseDto
    {
        public string Name { get; set; }

        public long ProjectId { get; set; }
    }
}

namespace DM.Domain.Models
{
    public class TemplateModel : BaseModel
    {
        public string Name { get; set; }

        public long ProjectId { get; set; }
    }
}

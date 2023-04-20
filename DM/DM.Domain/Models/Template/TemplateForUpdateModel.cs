namespace DM.Domain.Models
{
    public class TemplateForUpdateModel : BaseModel
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
    }
}

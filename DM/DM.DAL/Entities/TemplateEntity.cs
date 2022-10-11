using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;


namespace DM.DAL.Entities
{
    public class TemplateEntity : BaseEntity
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
        [Column(TypeName = "jsonb")]
        public JsonDocument RecordTemplate { get; set; }
    }
}

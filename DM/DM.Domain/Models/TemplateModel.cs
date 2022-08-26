using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;


namespace DM.Domain.Models
{
    public class TemplateModel
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public ProjectModel Project { get; set; } 
        [Column(TypeName = "jsonb")]
        public JObject RecordTemplate { get; set; }
    }

    public class TemplateModelForEdit
    {
        public long TemplateId { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public ProjectModel Project { get; set; }
        [Column(TypeName = "jsonb")]
        public JObject RecordTemplate { get; set; }
    }
}

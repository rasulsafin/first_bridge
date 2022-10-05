using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace WrapperDM.Models
{
    public class TemplateModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long ProjectId { get; set; }
        public ProjectModel? Project { get; set; } 
        [Column(TypeName = "jsonb")]
        public JObject? RecordTemplate { get; set; }
    }

    public class TemplateModelForEdit
    {
        public long TemplateId { get; set; }
        public string? Name { get; set; }
        public long ProjectId { get; set; }
        public ProjectModel? Project { get; set; }
        [Column(TypeName = "jsonb")]
        public JObject? RecordTemplate { get; set; }
    }
}

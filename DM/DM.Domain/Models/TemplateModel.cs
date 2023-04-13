using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace DM.Domain.Models
{
    public class TemplateModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public ProjectModel Project { get; set; }
        public List<FieldModel> Fields { get; set; }
        public List<ListFieldModel> ListFields { get; set; }
    }

    public class TemplateModelForEdit
    {
        public long TemplateId { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
    }
}

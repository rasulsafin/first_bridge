using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class TemplateForCreateModel : TemplateModel
    {
        public List<int> FieldIds { get; set; }
        public List<FieldModel> Fields { get; set; }

        public List<int> ListFieldIds { get; set; }
        public List<ListFieldModel> ListFields { get; set; }
    }
}

using System.Collections.Generic;

using DM.DAL.Enums;

namespace DM.Domain.Models
{
    public class ListFieldModel : BaseModel
    {
        public string Name { get; set; }
        public bool IsMandatory { get; set; }

        public FieldEnum Type { get; set; }

        public long? RecordId { get; set; }
        public long? TemplateId { get; set; }

        public List<int> ListIds { get; set; }
        public List<ListModel> Lists { get; set; }

        public ListFieldModel()
        {
            Type = FieldEnum.List;
            IsMandatory = false;
        }
    }
}

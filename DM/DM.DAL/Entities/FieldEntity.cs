using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Entities
{
    public class FieldEntity : BaseEntity
    {
        public string Name { get; set; }
        public FieldType Type { get; set; } = FieldType.Text;
        public bool IsMandatory { get; set; }
        public string Data { get; set; }

        public long? TemplateId { get; set; }
        public TemplateEntity Template { get; set; }

        public long? RecordId { get; set; }
        public RecordEntity Record { get; set; }
    }

    public class ListFieldEntity : BaseEntity
    {
        public string Name { get; set; }
        public FieldType Type { get; set; } = FieldType.List;
        public bool IsMandatory { get; set; }

        public List<ListEntity> Lists { get; set; }

        public long? TemplateId { get; set; }
        public TemplateEntity Template { get; set; }

        public long? RecordId { get; set; }
        public RecordEntity Record { get; set; }
    }

    public class ListEntity : BaseEntity
    {
        public long ListId { get; set; }
        public ListFieldEntity List { get; set; }
        public string Data { get; set; }
    }

    public enum FieldType
    {
        Text = 1, List = 2
    }
}

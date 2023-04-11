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
        public FieldType Type { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsListElement { get; set; }
        public string Data { get; set; }

        public List<TemplateEntity> Template { get; set; }
        public List<RecordEntity> Record { get; set; }
        public List<ListEntity> List { get; set; }

        public List<TemplateFieldEntity> TemplateField { get; set; }
        public List<RecordFieldEntity> RecordField { get; set; }
        public List<ListFieldEntity> ListField { get; set; }
    }

    public class ListFieldEntity : BaseEntity
    {
        public long FieldId { get; set; }
        public FieldEntity Field { get; set; }

        public long ListId { get; set; }
        public ListEntity List { get; set; }
        public bool IsListElement { get; set; }
    }

    public class ListEntity : BaseEntity
    {
        public string Name { get; set; }

        public List<FieldEntity> Field { get; set; }

        public List<ListFieldEntity> ListField { get; set; }
    }

    public enum FieldType
    {
        Text = 1, List = 2
    }
}

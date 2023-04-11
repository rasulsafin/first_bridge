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

        public List<TemplateEntity> Template { get; set; }
        public List<RecordEntity> Record { get; set; }

        public List<TemplateFieldEntity> TemplateField { get; set; }
        public List<RecordFieldEntity> RecordField { get; set; }
    }

    public class ListFieldEntity : BaseEntity
    {
        public string Name { get; set; }
        public FieldType Type { get; set; } = FieldType.List;
        public bool IsMandatory { get; set; }

        public List<ListEntity> ListData { get; set; }

        public List<TemplateEntity> Template { get; set; }
        public List<RecordEntity> Record { get; set; }

        public List<TemplateListEntity> TemplateList { get; set; }
        public List<RecordListEntity> RecordList { get; set; }
    }

    public class ListEntity : BaseEntity
    {
        public ListFieldEntity List { get; set; }
        public string Data { get; set; }
    }

    public enum FieldType
    {
        Text = 1, List = 2
    }
}

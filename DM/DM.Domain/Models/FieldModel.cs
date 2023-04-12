using DM.DAL.Entities;
using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class FieldModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public bool IsMandatory { get; set; }
        public string Data { get; set; }
    }

    public class ListFieldModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public bool IsMandatory { get; set; }
        public List<ListModel> Lists { get; set; }
    }

    public class ListModel
    {
        public long Id { get; set; }
        public long ListId { get; set; }
        public string Data { get; set; }
    }
}

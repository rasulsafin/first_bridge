﻿using DM.DAL.Entities;
using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class FieldModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; } = FieldType.Text;
        public bool IsMandatory { get; set; }
        public string Data { get; set; }
        public long? RecordId { get; set; }
        public long? TemplateId { get; set; }
    }

    public class ListFieldModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; } = FieldType.List;
        public bool IsMandatory { get; set; }
        public List<ListModel> Lists { get; set; }
        public long? RecordId { get; set; }
        public long? TemplateId { get; set; }
    }

    public class ListModel
    {
        public long Id { get; set; }
        public string Data { get; set; }
    }
}

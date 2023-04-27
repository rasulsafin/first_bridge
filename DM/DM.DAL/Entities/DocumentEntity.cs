﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using DM.DAL.Enums;

namespace DM.DAL.Entities
{
    [Table("Document")]
    public class DocumentEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Object { get; set; }
        public string Photo { get; set; }

        public StatusEnum Status { get; set; }
    }
}

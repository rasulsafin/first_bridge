﻿using DM.Entities;
using System.Collections.Generic;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Task associated with the project
    /// </summary>
    public class RecordEntity : BaseEntity
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
        public List<FieldsEntity> Fields { get; set; }
    }
}

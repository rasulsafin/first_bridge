﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Object .bim, ifc, and png for tests
    /// </summary>
    [Table("Item")]
    public class ItemEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string RelativePath { get; set; }
        [Required]
        public long ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
    }
}

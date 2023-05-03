using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DM.Common.Enums;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Task associated with the project
    /// </summary>
    [Table("Record")]
    public class Record : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //Исполнитель
        public string Executor { get; set; }
        public bool IsInArchive { get; set; }

        //Дата устранения
        public DateTime FixDate { get; set; }

        public StatusEnum Status { get; set; }
        public PriorityEnum Priority { get; set; }

        [Required]
        public long ProjectId { get; set; }
        public Project Project { get; set; }

        public long? TemplateId { get; set; }
        public Template Template { get; set; }

        public ICollection<Field> Fields { get; set; }
        public ICollection<ListField> ListFields { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}

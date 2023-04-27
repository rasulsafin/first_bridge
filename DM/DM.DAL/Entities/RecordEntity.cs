using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DM.DAL.Enums;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Task associated with the project
    /// </summary>
    [Table("Record")]
    public class RecordEntity : BaseEntity
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
        public ProjectEntity Project { get; set; }

        public long? TemplateId { get; set; }
        public TemplateEntity Template { get; set; }

        public List<FieldEntity> Fields { get; set; }
        public List<ListFieldEntity> ListFields { get; set; }
        public List<CommentEntity> Comments { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Task associated with the project
    /// </summary>
    [Table("Record")]
    public class RecordEntity : BaseEntity
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public List<FieldEntity> Fields { get; set; }
        public List<ListFieldEntity> ListFields { get; set; }
        public List<CommentEntity> Comments { get; set; }
    }
}

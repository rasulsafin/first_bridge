using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Task associated with the project
    /// </summary>
    public class RecordEntity : BaseEntity
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
        [Column(TypeName = "jsonb")]
        public JsonDocument Fields { get; set; }
        public List<CommentEntity> Comments { get; set; }
    }
}

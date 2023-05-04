using System;
using System.ComponentModel.DataAnnotations;

namespace DM.DAL.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public long? CreatedById { get; set; }
        public long? UpdatedById { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }
    }
}

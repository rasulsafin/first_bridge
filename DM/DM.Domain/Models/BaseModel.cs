using System;

namespace DM.Domain.Models
{
    public class BaseModel
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        public BaseModel()
        {
            CreatedAt = DateTime.Now;
        }
    }
}

using System;

namespace DM.Domain.DTO
{
    public class BaseDto
    {
        public long Id { get; set; }
        public long? CreatedById { get; set; }
        public long? UpdatedById { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public BaseDto()
        {
            CreatedAt = DateTime.Now;
        }
    }
}

﻿using System;

namespace DM.Domain.Models
{
    public class BaseModel
    {
        public long Id { get; set; }
        public long? CreatedById { get; set; }
        public long? UpdatedById { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public BaseModel()
        {
            CreatedAt = DateTime.Now;
        }
    }
}

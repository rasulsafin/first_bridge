﻿using DM.Common.Enums;

namespace DM.Domain.DTO
{
    public class DocumentDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Object { get; set; }
        public string Photo { get; set; }

        public StatusEnum Status { get; set; }
    }
}

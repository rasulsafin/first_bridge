﻿using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class TemplateForCreateDto : TemplateDto
    {
        public List<int> FieldIds { get; set; }
        public List<FieldDto> Fields { get; set; }

        public List<int> ListFieldIds { get; set; }
        public List<ListFieldDto> ListFields { get; set; }
    }
}
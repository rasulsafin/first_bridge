using System.Collections.Generic;

namespace DM.Domain.DTO
{
    public class RecordForCreateDto : RecordDto
    {
        public ICollection<int> ListChildIds { get; set; }
    }
}
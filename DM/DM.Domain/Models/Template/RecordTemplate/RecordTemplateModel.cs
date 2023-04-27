using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class RecordTemplateModel
    {
        public List<int> RecordIds { get; set; }
        public List<RecordModel> Records { get; set; }
    }
}

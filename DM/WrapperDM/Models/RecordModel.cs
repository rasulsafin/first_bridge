using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace Wrapper.Models
{
    public class RecordModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long ProjectId { get; set; }
        [Column(TypeName = "jsonb")]
        public JObject? Fields { get; set; }
    }
}

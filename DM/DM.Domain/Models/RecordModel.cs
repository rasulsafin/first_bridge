using DM.DAL.Entities;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace DM.Domain.Models
{
    public class RecordModel
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
        [Column(TypeName = "jsonb")]
        public JObject Fields { get; set; }
    }
}

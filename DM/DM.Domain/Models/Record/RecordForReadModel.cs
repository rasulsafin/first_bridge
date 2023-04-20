using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Models
{
    public class RecordForReadModel : RecordModel
    {
        public List<CommentForReadModel> Comments { get; set; }
    }
}

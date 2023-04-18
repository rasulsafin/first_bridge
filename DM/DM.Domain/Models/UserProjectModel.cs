using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Models
{
    public class UserProjectModel
    {
        public long UserId { get; set; }
        public UserModel User { get; set; }

        public long ProjectId { get; set; }
        public ProjectModel Project { get; set; }
    }
}

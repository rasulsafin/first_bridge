using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Models
{
    public class UserForReadModel : UserModel
    {
        public List<ProjectModel> Projects { get; set; }
    }
}

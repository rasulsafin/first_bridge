using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Models
{
    public class ProjectForReadModel : ProjectModel
    {
        public List<ItemModel> Items { get; set; }
        public List<UserModel> Users { get; set; }
    }
}

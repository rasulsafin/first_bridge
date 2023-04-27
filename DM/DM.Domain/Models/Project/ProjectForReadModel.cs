using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class ProjectForReadModel : ProjectModel
    {
        public List<int> ItemIds { get; set; }
        public List<ItemModel> Items { get; set; }

        public List<int> UserIds { get; set; }
        public List<UserModel> Users { get; set; }
    }
}

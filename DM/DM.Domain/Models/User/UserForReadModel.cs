using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class UserForReadModel : UserModel
    {
        public List<int> ProjectsIds { get; set; }
        public List<ProjectModel> Projects { get; set; }
    }
}

using DM.Entities;
using System.Collections.Generic;

namespace DM.DAL.Entities
{
    public class ProjectEntity : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<ObjectiveEntity> Objectives { get; set; }

        public ICollection<UserProjectEntity> Users { get; set; }
    }
}

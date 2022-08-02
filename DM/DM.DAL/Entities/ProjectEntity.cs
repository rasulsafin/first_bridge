using DM.Entities;
using System.Collections.Generic;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Working project
    /// </summary>
    public class ProjectEntity : BaseEntity
    {
        public string Title { get; set; }

 //       public List<ObjectiveEntity> Objectives { get; set; }

 //       public List<long>? UserIds { get; set; }
 //       public List<UserProjectEntity> Users { get; set; }
        public List<ItemEntity> Items { get; set; }
        public List<RecordEntity> Records { get; set; }
    }
}

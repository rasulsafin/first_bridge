using DM.DAL.Entities;
using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class ProjectModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> User { get; set; }

        //    public List<ItemModel> Items { get; set; }
        //    public List<ObjectiveEntity> Objectives { get; set; }
    }
}

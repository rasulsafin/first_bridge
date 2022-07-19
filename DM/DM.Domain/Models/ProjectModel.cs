using DM.DAL.Entities;
using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class ProjectModel
    {
        public string Title { get; set; }
        public List<UserModel> User { get; set; }

        //    public List<ItemModel> Items { get; set; }
        //    public List<ObjectiveEntity> Objectives { get; set; }
    }
}

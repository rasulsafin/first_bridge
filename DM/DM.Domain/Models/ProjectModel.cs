using System;
using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class ProjectModel
    {
        public long Id { get; set; }
        public long OrganizationId { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }

        public List<ItemModel> Items { get; set; }
        public List<UserModel> Users { get; set; }
        //public List<ObjectiveEntity> Objectives { get; set; }

        public bool IsInArchive { get; set; }
    }
}

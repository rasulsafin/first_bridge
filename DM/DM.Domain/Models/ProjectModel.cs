using DM.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Models
{
    public class ProjectModel
    {
        public string Title { get; set; }

        public ICollection<ObjectiveEntity> Objectives { get; set; }

        public ICollection<UserProjectEntity> Users { get; set; }
    }
}

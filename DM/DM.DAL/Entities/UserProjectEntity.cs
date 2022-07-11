using DM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Entities
{
    /// <summary>
    /// many to many
    /// </summary>
    public class UserProjectEntity : BaseEntity
    {
        public int UserId { get; set; }

        public UserEntity User { get; set; }

        public int ProjectId { get; set; }

        public ProjectEntity Project { get; set; }
    }
}

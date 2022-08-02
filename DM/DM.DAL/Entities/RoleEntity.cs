using DM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Entities
{
    /// <summary>
    /// User role
    /// </summary>
    public class RoleEntity
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<UserEntity> Users { get; set; }
    }
}

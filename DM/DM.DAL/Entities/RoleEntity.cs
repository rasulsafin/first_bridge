using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Entities
{
    public class RoleEntity
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<UserRoleEntity> Users { get; set; }
    }
}

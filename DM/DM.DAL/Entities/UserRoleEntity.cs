using DM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Entities
{
    public class UserRoleEntity : BaseEntity
    {
        public long UserId { get; set; }

        public UserEntity User { get; set; }

        public int RoleId { get; set; }

        public RoleEntity Role { get; set; }
    }
}

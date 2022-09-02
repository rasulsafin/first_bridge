using System.Collections.Generic;
using DM.Entities;

namespace DM.DAL.Entities
{
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }
        public List<UserEntity> Users { get; set; }
    }
}
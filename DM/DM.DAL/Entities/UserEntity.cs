using DM.DAL.Entities;
using System;
using System.Collections.Generic;

namespace DM.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public ICollection<ObjectiveEntity> Objectives { get; set; }

        public ICollection<UserProjectEntity> Projects { get; set; }

        public ICollection<UserRoleEntity> Roles { get; set; }

    }
}
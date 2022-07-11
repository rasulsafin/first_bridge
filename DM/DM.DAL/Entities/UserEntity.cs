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
        public List<ObjectiveEntity> Objectives { get; set; }

        public List<UserProjectEntity> Projects { get; set; }

        public List<UserRoleEntity> Roles { get; set; }

    }
}
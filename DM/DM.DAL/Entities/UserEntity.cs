using DM.DAL.Entities;
using System;
using System.Collections.Generic;

namespace DM.Entities
{
    public class UserEntity : BaseEntity
    {
        /// TODO: PASSWORDHASH
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<UserProjectEntity> Projects { get; set; }
        public List<UserRoleEntity> Roles { get; set; }
        //    public List<ObjectiveEntity> Objectives { get; set; }
    }
}
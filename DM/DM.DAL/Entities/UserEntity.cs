using System;
using System.Collections.Generic;

namespace DM.DAL.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
        public RoleEntity Role { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Организация 
        /// </summary>
        public long OrganizationId { get; set; }
        public OrganizationEntity Organization { get; set; }
        
        public List<UserEntity> Users { get; set; }
        
        public List<UserProjectEntity> UserProjects { get; set; }
        
        public List<ProjectEntity> Projects { get; set; }
    }
}
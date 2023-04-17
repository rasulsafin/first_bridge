using DM.DAL.Entities;
using System;
using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
        public RoleModel Role { get; set; }
        public string Position { get; set; }
        public long OrganizationId { get; set; }

        public List<ProjectModel> Projects { get; set; }
    }
    
    public class UserModelForUpdate
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
        public RoleModel Role { get; set; }
        public string Position { get; set; }
        public long OrganizationId { get; set; }
        
        public List<ProjectModel> Projects { get; set; }
    }

}
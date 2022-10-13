using DM.DAL.Entities;
using System;

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
        public string Roles { get; set; }
        public DateTime Birthdate { get; set; }
        public string Snils { get; set; }
        public string Position { get; set; }
        public long OrganizationId { get; set; }
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
        public string Roles { get; set; }
        public DateTime Birthdate { get; set; }
        public string Snils { get; set; }
        public string Position { get; set; }
        public long OrganizationId { get; set; }
    }

}
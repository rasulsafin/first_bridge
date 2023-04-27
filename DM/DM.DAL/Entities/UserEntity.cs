using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace DM.DAL.Entities
{
    [Table("User")]
    [Index("Email", IsUnique = true, Name = "User_Email")]
    [Index("Login", IsUnique = true, Name = "User_Login")]
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Position { get; set; }

        [Required]
        public long RoleId { get; set; }
        public RoleEntity Role { get; set; }

        [Required]
        public long OrganizationId { get; set; }
        public OrganizationEntity Organization { get; set; }

        public List<UserProjectEntity> UserProjects { get; set; }
    }
}
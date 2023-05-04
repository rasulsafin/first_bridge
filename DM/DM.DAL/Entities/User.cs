using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace DM.DAL.Entities
{
    [Table("User")]
    [Index("Email", IsUnique = true, Name = "User_Email")]
    [Index("Login", IsUnique = true, Name = "User_Login")]
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string HashedPassword { get; set; }
        public string Position { get; set; }

        [Required]
        public long RoleId { get; set; }
        public Role Role { get; set; }

        [Required]
        public long OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public ICollection<UserProject> UserProjects { get; set; }
    }
}
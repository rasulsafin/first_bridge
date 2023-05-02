using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace DM.DAL.Entities
{
    [Table("Organization")]
    [Index("Email", IsUnique = true, Name = "Organization_Email")]
    [Index("Name", IsUnique = true, Name = "Organization_Name")]
    [Index("Inn", IsUnique = true, Name = "Organization_Inn")]
    [Index("Ogrn", IsUnique = true, Name = "Organization_Ogrn")]
    public class Organization : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Inn { get; set; }
        public string Ogrn { get; set; }
        public string Kpp { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Project> Projects { get; set; }

    }
}

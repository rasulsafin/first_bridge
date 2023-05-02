using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    public class Objective : BaseEntity
    {
        public string Role { get; set; }
        public string Name { get; set; }
    }
}

using DM.DAL.Entities;

namespace DM.Domain.Models
{
    public class PermissionModel
    {
        public long? UserId { get; set; }
        public long ObjectId { get; set; }
        public PermissionType Type { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}

namespace DM.DAL.Entities
{
    public class PermissionEntity : BaseEntity
    {
        public long? UserId { get; set; }
        public UserEntity User { get; set; }
        public long ObjectId { get; set; }
        public PermissionType Type { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }

    public enum PermissionType
    {
        Project = 1, Item = 2, Record = 3
    }
}
using DM.Entities;

namespace DM.DAL.Entities
{
    public class FieldsEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public state State { get; set; }
        public UserEntity Issuer { get; set; }
        public UserEntity Assignee { get; set; }
    }

    public enum state
    {
        created = 1,
        updated = 2,
        deleted = 3
    }
}

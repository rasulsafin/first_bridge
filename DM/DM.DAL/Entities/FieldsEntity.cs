using DM.Entities;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Fields of Record
    /// </summary>
    public class FieldsEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FieldState State { get; set; }
        public int IssuerId { get; set; }
        public int AssigneeId { get; set; }
    }

    public enum FieldState
    {
        created = 1,
        updated = 2,
        deleted = 3
    }
}

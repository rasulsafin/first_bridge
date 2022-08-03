using DM.DAL.Entities;

namespace DM.Domain.Models
{
    public class FieldsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FieldState State { get; set; }
        public int IssuerId { get; set; }
        public int AssigneeId { get; set; }
    }

    public class FieldsModelForCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FieldState State { get; set; }
        public int IssuerId { get; set; }
        public int AssigneeId { get; set; }
    }
}

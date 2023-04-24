namespace DM.DAL.Entities
{
    public class ListEntity : BaseEntity
    {
        public long ListId { get; set; }
        public ListFieldEntity List { get; set; }
        public string Data { get; set; }
    }
}

namespace WrapperDM.Entities
{
    /// <summary>
    /// Object .bim, ifc, and png for tests
    /// </summary>
    public class ItemEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? RelativePath { get; set; }
        public ProjectEntity? Project { get; set; }
    }
}

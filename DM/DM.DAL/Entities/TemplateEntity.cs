using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;


namespace DM.DAL.Entities
{
    public class TemplateEntity : BaseEntity
    {
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public ProjectEntity Project { get; set; }

        public List<FieldEntity> Fields { get; set; }
        public List<ListFieldEntity> ListFields { get; set; }
    }
}

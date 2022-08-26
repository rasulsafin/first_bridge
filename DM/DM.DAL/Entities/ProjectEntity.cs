using DM.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Working project
    /// </summary>
    public class ProjectEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ItemEntity> Items { get; set; }
        public List<RecordEntity> Records { get; set; }
        public List<TemplateEntity> Template { get; set; }

    }
}

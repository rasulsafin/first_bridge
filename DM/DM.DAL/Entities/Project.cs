using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Working project
    /// </summary>
    [Table("Project")]
    public class Project : BaseEntity
    {
        public string Title { get; set; }
        public bool IsInArchive { get; set; }

        [Required]
        public long OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public ICollection<Item> Items { get; set; }
        public ICollection<Record> Records { get; set; }
        public ICollection<Template> Templates { get; set; }

        //public List<RecordTemplateEntity> RecordTemplates { get; set; }
        //public List<DocumentTemplateEntity> DocumentTemplates { get; set; }

        public ICollection<UserProject> UserProjects { get; set; }
    }
}

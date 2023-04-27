using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Working project
    /// </summary>
    [Table("Project")]
    public class ProjectEntity : BaseEntity
    {
        public string Title { get; set; }
        public bool IsInArchive { get; set; }

        [Required]
        public long OrganizationId { get; set; }
        public OrganizationEntity Organization { get; set; }

        public List<ItemEntity> Items { get; set; }
        public List<RecordEntity> Records { get; set; }
        public List<TemplateEntity> Templates { get; set; }

        //public List<RecordTemplateEntity> RecordTemplates { get; set; }
        //public List<DocumentTemplateEntity> DocumentTemplates { get; set; }

        public List<UserProjectEntity> UserProjects { get; set; }
    }
}

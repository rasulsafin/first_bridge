using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Working project
    /// </summary>
    [Table("Project")]
    public class ProjectEntity : BaseEntity
    {
        /// <summary>
        /// Название проекта
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Users of project
        /// </summary>
        [NotMapped]
        public List<int> UserProjectIds { get; set; }
        public List<UserProjectEntity> UserProjects { get; set; }
        /// <summary>
        /// Документы проекта
        /// </summary>
        [NotMapped]
        public List<int> ItemIds { get; set; }
        public List<ItemEntity> Items { get; set; }
        /// <summary>
        /// Записи проекта
        /// </summary>
        [NotMapped]
        public List<int> RecordIds { get; set; }
        public List<RecordEntity> Records { get; set; }
        /// <summary>
        /// Шаблон для создания записи о проекте
        /// </summary>
        public List<TemplateEntity> Template { get; set; }
        /// <summary>
        /// Организация
        /// </summary>
        public long OrganizationId { get; set; }
        public OrganizationEntity Organization { get; set; }
        public bool IsInArchive { get; set; }
    }
}

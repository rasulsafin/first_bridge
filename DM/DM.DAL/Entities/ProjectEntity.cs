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
        /// <summary>
        /// Название проека
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Описание проекта
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Документы проекта
        /// </summary>
        public List<ItemEntity> Items { get; set; }
        /// <summary>
        /// Записи проекта
        /// </summary>
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

    }
}

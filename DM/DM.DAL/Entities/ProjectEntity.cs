﻿using System.Collections.Generic;

namespace DM.DAL.Entities
{
    /// <summary>
    /// Working project
    /// </summary>
    public class ProjectEntity : BaseEntity
    {
        /// <summary>
        /// Название проекта
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Users of project
        /// </summary>
        public List<UserProjectEntity> UserProjects { get; set; }
        
        public List<UserEntity> Users { get; set; }
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
        public bool IsInArchive { get; set; }
    }
}

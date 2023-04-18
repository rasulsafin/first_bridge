using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace DM.DAL.Entities
{
    [Table("Organization")]
    [Index("Email", IsUnique = true, Name = "Organization_Email")]
    [Index("Name", IsUnique = true, Name = "Organization_Name")]
    [Index("Inn", IsUnique = true, Name = "Organization_Inn")]
    [Index("Ogrn", IsUnique = true, Name = "Organization_Ogrn")]
    public class OrganizationEntity : BaseEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// ОГРН
        /// </summary>
        public string Ogrn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        public string Kpp { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Список пользователей организации
        /// </summary>
        public List<UserEntity> Users { get; set; }

        /// <summary>
        /// Коллекция проектов
        /// </summary>
        public List<ProjectEntity> Projects { get; set; }

    }
}

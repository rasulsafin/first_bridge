using System.Collections.Generic;

namespace WrapperDM.Entities
{
    public class OrganizationEntity
    {
        public long Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string? Inn { get; set; }

        /// <summary>
        /// ОГРН
        /// </summary>
        public string? Ogrn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        public string? Kpp { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Список пользователей организации
        /// </summary>
        public List<UserEntity>? Users { get; set; }

        /// <summary>
        /// Коллекция проектов
        /// </summary>
        public List<ProjectEntity>? Projects { get; set; }

    }
}

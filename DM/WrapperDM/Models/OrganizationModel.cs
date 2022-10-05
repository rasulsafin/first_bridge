using System.Collections.Generic;

namespace WrapperDM.Models
{
    public class OrganizationModel
    {
        public long Id { get; set; }

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
        public List<UserModel>? Users { get; set; }

        /// <summary>
        /// Коллекция проектов
        /// </summary>
        public List<ProjectModel>? Projects { get; set; }
    }

    public class OrganizationModelForCreate
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Inn { get; set; }
        public string? Ogrn { get; set; }
        public string? Kpp { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}

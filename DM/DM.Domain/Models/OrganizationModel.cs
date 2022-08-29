using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Models
{
    public class OrganizationModel
    {
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
        public List<UserModel> Users { get; set; }

        /// <summary>
        /// Коллекция проектов
        /// </summary>
        public List<ProjectModel> Projects { get; set; }
    }
}

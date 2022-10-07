using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Models
{
    public class OrganizationModel : OrganizationModelForCreate
    {
        public long Id { get; set; }

        /// <summary>
        /// Список пользователей организации
        /// </summary>
        public List<UserModel> Users { get; set; }

        /// <summary>
        /// Коллекция проектов
        /// </summary>
        public List<ProjectModel> Projects { get; set; }
    }
    
    public class OrganizationModelForUpdate : OrganizationModelForCreate
    {
        public long Id { get; set; }
    }

    public class OrganizationModelForCreate
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Inn { get; set; }
        public string Ogrn { get; set; }
        public string Kpp { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

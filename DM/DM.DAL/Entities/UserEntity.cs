using DM.DAL.Entities;
using System;
using System.Collections.Generic;

namespace DM.Entities
{
    public class UserEntity : BaseEntity
    {
        /// TODO: PASSWORDHASH
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEntity Roles { get; set; }
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// СНИЛС
        /// </summary>
        public string Snils { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Организация
        /// </summary>
        public long OrganizationId { get; set; }
        public OrganizationEntity Organization { get; set; }

    }
}
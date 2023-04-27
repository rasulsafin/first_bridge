﻿using System.Collections.Generic;

namespace DM.Domain.Models
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }

        public long RoleId { get; set; }
        public long OrganizationId { get; set; }
    }
}
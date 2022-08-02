﻿using DM.DAL.Entities;

namespace DM.Domain.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DM.Common.Enums;

namespace DM.DAL.Entities
{
    [Table("Permission")]
    public class Permission : BaseEntity
    {
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }

        public PermissionEnum Type { get; set; }

        [Required]
        public long RoleId { get; set; }
        public Role Role { get; set; }
    }
}
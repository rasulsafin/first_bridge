using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Models
{
    public class FieldsModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FieldState State { get; set; }
        public UserModel Issuer { get; set; }
        public UserModel Assignee { get; set; }
    }

    public enum FieldState
    {
        created = 1,
        updated = 2,
        deleted = 3
    }
}

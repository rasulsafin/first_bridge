using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.DTO
{
    public class RoleForUpdateDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

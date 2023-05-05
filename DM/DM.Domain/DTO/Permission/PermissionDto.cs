using DM.Common.Enums;
using DM.Domain.DTO;

namespace DM.Domain.DTO
{
    public class PermissionDto : BaseDto
    {
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }

        public PermissionEnum Type { get; set; }

        public long RoleId { get; set; }
    }
}

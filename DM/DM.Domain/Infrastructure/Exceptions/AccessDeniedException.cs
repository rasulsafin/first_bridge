using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Infrastructure.Exceptions
{
    public class AccessDeniedException : DocumentManagementException
    {
        public AccessDeniedException(string message)
            : base(message)
        {
        }
    }
}

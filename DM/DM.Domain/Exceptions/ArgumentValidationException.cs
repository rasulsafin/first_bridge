using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Exceptions
{
    public class ArgumentValidationException : DocumentManagementException
    {
        public ArgumentValidationException(string details, string title = null, IReadOnlyDictionary<string, string[]> errors = null)
            : base(title ?? "Validation Error", details, errors)
        {
        }
    }
}

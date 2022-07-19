using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Domain.Exceptions
{
    public abstract class ANotFoundException : DocumentManagementException
    {
        protected ANotFoundException(string details, string title = null, IReadOnlyDictionary<string, string[]> errors = null)
            : base(title ?? "Not found", details, errors)
        {
        }
    }
}

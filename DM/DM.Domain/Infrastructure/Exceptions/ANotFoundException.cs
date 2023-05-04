using System.Collections.Generic;

namespace DM.Domain.Infrastructure.Exceptions
{
    public abstract class ANotFoundException : DocumentManagementException
    {
        protected ANotFoundException(string details, string title = null, IReadOnlyDictionary<string, string[]> errors = null)
            : base(title ?? "Not found", details, errors)
        {
        }
    }
}

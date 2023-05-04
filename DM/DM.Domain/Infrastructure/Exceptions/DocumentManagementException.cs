using System;
using System.Collections.Generic;

namespace DM.Domain.Infrastructure.Exceptions
{
    /// <summary>
    /// Raised on connection errors.
    /// </summary>
    public class DocumentManagementException : Exception
    {
        /// <summary>
        /// Error details, e.g. server exception message. May be null.
        /// </summary>
        public string Details { get; }

        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public DocumentManagementException(string title, string details = null, IReadOnlyDictionary<string, string[]> errors = null)
            : base(title)
        {
            Details = details;
            Errors = errors as IReadOnlyDictionary<string, string[]>;
        }
    }
}

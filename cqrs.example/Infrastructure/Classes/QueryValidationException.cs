using System;

namespace Cqrs.Example.Infrastructure
{
    public class QueryValidationException : Exception
    {
        public QueryValidationException() : base() { }

        public QueryValidationException(string message) : base(message) { }

        public QueryValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}

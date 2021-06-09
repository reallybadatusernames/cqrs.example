using System;

namespace Cqrs.Example.Infrastructure
{
    public class QueryExecutionException : Exception
    {
        public QueryExecutionException() : base() { }

        public QueryExecutionException(string message) : base(message) { }

        public QueryExecutionException(string message, Exception innerException) : base(message, innerException) { }
    }
}

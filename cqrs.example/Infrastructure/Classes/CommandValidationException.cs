using System;

namespace Cqrs.Example.Infrastructure
{
    public class CommandValidationException : Exception
    {
        public CommandValidationException() : base() { }

        public CommandValidationException(string message) : base(message) { }

        public CommandValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}

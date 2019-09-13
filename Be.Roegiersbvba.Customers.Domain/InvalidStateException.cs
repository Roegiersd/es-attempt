using System;
using System.Runtime.Serialization;

namespace be.roegiersbvba.Customers.Domain
{
    [Serializable]
    internal class InvalidStateException : Exception
    {
        public InvalidStateException()
        {
        }

        public InvalidStateException(string message) : base(message)
        {
        }

        public InvalidStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
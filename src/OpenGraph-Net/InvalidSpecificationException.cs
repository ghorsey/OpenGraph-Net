using System;

namespace OpenGraph_Net
{
    [Serializable]
    public class InvalidSpecificationException : Exception
    {
        public InvalidSpecificationException() { }
        public InvalidSpecificationException(string message) : base(message) { }
        public InvalidSpecificationException(string message, Exception inner) : base(message, inner) { }
        protected InvalidSpecificationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

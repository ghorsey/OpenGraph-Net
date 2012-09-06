using System;

namespace OpenGraph_Net
{
    [Serializable]
    public class ReadOnlyDictionaryException : NotSupportedException
    {
        public ReadOnlyDictionaryException() : this( "This is a read-only dictionary") { }
        public ReadOnlyDictionaryException(string message) : base(message) { }
        public ReadOnlyDictionaryException(string message, Exception inner) : base(message, inner) { }
        protected ReadOnlyDictionaryException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

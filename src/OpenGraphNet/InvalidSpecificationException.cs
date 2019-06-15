namespace OpenGraphNet
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// An invalid specification exception.
    /// </summary>
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class InvalidSpecificationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSpecificationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InvalidSpecificationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSpecificationException"/> class.
        /// </summary>
        public InvalidSpecificationException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSpecificationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public InvalidSpecificationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

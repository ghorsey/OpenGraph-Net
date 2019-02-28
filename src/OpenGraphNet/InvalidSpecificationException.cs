namespace OpenGraphNet
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// An invalid specification exception
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
    }
}

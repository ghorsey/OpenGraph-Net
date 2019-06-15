namespace OpenGraphNet.Metadata
{
    /// <summary>
    /// Represents a null <see cref="Metadata"/>.
    /// </summary>
    /// <seealso cref="OpenGraphNet.Metadata" />
    public sealed class NullMetadata : StructuredMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullMetadata"/> class.
        /// </summary>
        public NullMetadata()
            : base(null, string.Empty, string.Empty)
        {
        }
    }
}

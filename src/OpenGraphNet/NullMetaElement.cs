namespace OpenGraphNet
{
    /// <summary>
    /// Represents a null <see cref="MetaElement"/>
    /// </summary>
    /// <seealso cref="OpenGraphNet.MetaElement" />
    public sealed class NullMetaElement : StructuredMetaElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullMetaElement"/> class.
        /// </summary>
        public NullMetaElement()
            : base(null, string.Empty, string.Empty)
        {
        }
    }
}

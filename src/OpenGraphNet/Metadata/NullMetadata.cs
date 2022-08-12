namespace OpenGraphNet.Metadata;

/// <summary>
/// Represents a null <see cref="MetadataBase"/>.
/// </summary>
/// <seealso cref="OpenGraphNet.Metadata" />
public sealed class NullMetadata : StructuredMetadata
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NullMetadata"/> class.
    /// </summary>
    public NullMetadata()
        : base(OpenGraphNamespace.Empty, string.Empty, string.Empty)
    {
    }
}

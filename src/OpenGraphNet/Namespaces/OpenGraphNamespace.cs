namespace OpenGraphNet.Namespaces;

/// <summary>
/// An OpenGraph Namespace.
/// </summary>
public class OpenGraphNamespace
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphNamespace"/> class.
    /// </summary>
    /// <param name="prefix">The prefix.</param>
    /// <param name="schemaUri">The schema URI.</param>
    public OpenGraphNamespace(string prefix, Uri schemaUri)
    {
        this.Prefix = prefix;
        this.SchemaUri = schemaUri;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGraphNamespace"/> class.
    /// </summary>
    /// <param name="prefix">The prefix.</param>
    /// <param name="schemaUri">The schema URI.</param>
    public OpenGraphNamespace(string prefix, string schemaUri)
        : this(prefix, new Uri(schemaUri, UriKind.RelativeOrAbsolute))
    {
    }

    /// <summary>
    /// Gets an empty OpenGraph Namespace.
    /// </summary>
    /// <value>The empty.</value>
    public static OpenGraphNamespace Empty => new OpenGraphNamespace(string.Empty, "schema://empty");

    /// <summary>
    /// Gets the prefix.
    /// </summary>
    /// <value>
    /// The prefix.
    /// </value>
    public string Prefix { get; }

    /// <summary>
    /// Gets the schema URI.
    /// </summary>
    /// <value>
    /// The schema URI.
    /// </value>
    public Uri SchemaUri { get; }

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return string.Concat(this.Prefix, ": ", this.SchemaUri.ToString());
    }
}

namespace OpenGraphNet
{
    using System;

    /// <summary>
    /// An OpenGraph Namespace
    /// </summary>
    public class Namespace
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace"/> class.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="schemaUri">The schema URI.</param>
        public Namespace(string prefix, Uri schemaUri)
        {
            this.Prefix = prefix;
            this.SchemaUri = schemaUri;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace"/> class.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="schemaUri">The schema URI.</param>
        public Namespace(string prefix, string schemaUri)
            : this(prefix, new Uri(schemaUri, UriKind.RelativeOrAbsolute))
        {
        }

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
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Concat(this.Prefix, ": ", this.SchemaUri.ToString());
        }
    }
}

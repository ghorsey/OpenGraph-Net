namespace OpenGraphNet.Namespaces
{
    using System.Collections.Generic;

    /// <summary>
    /// The list of known supported Open Graph namespaces.
    /// </summary>
    /// <seealso cref="OpenGraphNamespace" />
    public sealed class RegistryNamespace : OpenGraphNamespace
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryNamespace"/> class.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="schemaUri">The schema URI.</param>
        /// <param name="requiredElements">The required elements.</param>
        public RegistryNamespace(string prefix, string schemaUri, params string[] requiredElements)
            : base(prefix, schemaUri)
        {
            this.RequiredElements = new List<string>(requiredElements);
        }

        /// <summary>
        /// Gets the required elements.
        /// </summary>
        /// <value>The required elements.</value>
        public IList<string> RequiredElements { get; }
    }
}
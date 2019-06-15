namespace OpenGraphNet.Metadata
{
    using System.Collections.Generic;
    using System.Linq;

    using OpenGraphNet.Namespaces;

    /// <summary>
    /// Adds some helpful extension methods.
    /// </summary>
    public static class MetadataHelperExtensions
    {
        /// <summary>
        /// Values the specified metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The value of the first item in the list.</returns>
        public static string Value(this IList<StructuredMetadata> metadata) => Value(metadata.FirstOrDefault());

        /// <summary>
        /// Values the specified metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The value of the first item in the list.</returns>
        public static string Value(this IList<PropertyMetadata> metadata) => Value(metadata.FirstOrDefault());

        /// <summary>
        /// Namespaces the specified metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The namespace of the first item in the list.</returns>
        public static OpenGraphNamespace Namespace(this IList<StructuredMetadata> metadata) => Namespace(metadata.FirstOrDefault());

        /// <summary>
        /// Namespaces the specified metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The namespace of the first item in the list.</returns>
        public static OpenGraphNamespace Namespace(this IList<PropertyMetadata> metadata) => Namespace(metadata.FirstOrDefault());

        /// <summary>
        /// Names the specified metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The name of the first item in the list.</returns>
        public static string Name(this IList<StructuredMetadata> metadata) => Name(metadata.FirstOrDefault());

        /// <summary>
        /// Names the specified metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The name of the first item in the list.</returns>
        public static string Name(this IList<PropertyMetadata> metadata) => Name(metadata.FirstOrDefault());

        /// <summary>
        /// Values the specified metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The value of the item, or an empty string when null.</returns>
        private static string Value(this MetadataBase metadata) => (metadata ?? new NullMetadata()).Value ?? string.Empty;

        /// <summary>
        /// Namespaces the specified metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The namespace of the item when present; otherwise null.</returns>
        private static OpenGraphNamespace Namespace(this MetadataBase metadata) => (metadata ?? new NullMetadata()).Namespace;

        /// <summary>
        /// Names the specified metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The name of the item when present; otherwise an empty string.</returns>
        private static string Name(this MetadataBase metadata) => (metadata ?? new NullMetadata()).Name ?? string.Empty;
    }
}

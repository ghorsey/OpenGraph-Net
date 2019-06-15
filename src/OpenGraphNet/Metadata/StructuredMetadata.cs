namespace OpenGraphNet.Metadata
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    using HtmlAgilityPack;

    using OpenGraphNet.Namespaces;

    /// <summary>
    /// A root structured element.
    /// </summary>
    /// <seealso cref="OpenGraphNet.Metadata" />
    [DebuggerDisplay("{" + nameof(Name) + "}: {" + nameof(Value) + "}")]
    public class StructuredMetadata : MetadataBase
    {
        /// <summary>
        /// The internal properties.
        /// </summary>
        private readonly Dictionary<string, IList<PropertyMetadata>> internalProperties = new Dictionary<string, IList<PropertyMetadata>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="StructuredMetadata"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public StructuredMetadata(OpenGraphNamespace ns, string name, string value)
            : base(ns, name, value)
        {
        }

        /// <summary>
        /// Gets the child elements.
        /// </summary>
        /// <value>
        /// The child elements.
        /// </value>
        public IDictionary<string, IList<PropertyMetadata>> Properties => new ReadOnlyDictionary<string, IList<PropertyMetadata>>(this.internalProperties);

        /// <summary>
        /// Adds the property.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void AddProperty(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            name = name.Replace(this.Namespace + ":", string.Empty);
            name = name.Replace(this.Name + ":", string.Empty);
            var propertyElement = new PropertyMetadata(this, this.Namespace, name, value);
            this.AddProperty(propertyElement);
        }

        /// <summary>
        /// Adds the property.
        /// </summary>
        /// <param name="element">The element.</param>
        public void AddProperty(PropertyMetadata element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.ParentElement = this;
            element.Namespace = this.Namespace;

            if (this.internalProperties.ContainsKey(element.Name))
            {
                this.internalProperties[element.Name].Add(element);
            }
            else
            {
                this.internalProperties.Add(element.Name, new List<PropertyMetadata>() { element });
            }
        }

        /// <summary>
        /// Determines whether the specified property key is a property of this element.
        /// </summary>
        /// <param name="propertyKey">The property key.</param>
        /// <returns>
        ///   <c>true</c> if the specified property key is a child property of this element; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMyProperty(string propertyKey)
        {
            if (propertyKey == null)
            {
                throw new ArgumentNullException(nameof(propertyKey));
            }

            var myKey = string.Concat(this.Namespace.Prefix, ":", this.Name);
            return propertyKey.StartsWith(myKey, StringComparison.OrdinalIgnoreCase) && !string.Equals(propertyKey, myKey, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Creates the document.
        /// </summary>
        /// <returns>
        /// The HTML snippet that represents this element.
        /// </returns>
        protected internal override HtmlDocument CreateDocument()
        {
            var doc = base.CreateDocument();

            var elements = this.Properties.SelectMany(p => p.Value);

            foreach (var metaElement in elements)
            {
                doc.DocumentNode.AppendChild(metaElement.CreateDocument().DocumentNode);
            }

            return doc;
        }
    }
}

namespace OpenGraphNet
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using HtmlAgilityPack;

    /// <summary>
    /// A root structured element
    /// </summary>
    /// <seealso cref="OpenGraphNet.MetaElement" />
    public class StructuredMetaElement : MetaElement
    {
        /// <summary>
        /// The internal properties
        /// </summary>
        private Dictionary<string, IList<PropertyMetaElement>> internalProperties = new Dictionary<string, IList<PropertyMetaElement>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="StructuredMetaElement"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public StructuredMetaElement(Namespace ns, string name, string value)
            : base(ns, name, value)
        {
        }

        /// <summary>
        /// Gets the child elements.
        /// </summary>
        /// <value>
        /// The child elements.
        /// </value>
        public IDictionary<string, IList<PropertyMetaElement>> Properties => new ReadOnlyDictionary<string, IList<PropertyMetaElement>>(this.internalProperties);

        /// <summary>
        /// Adds the property.
        /// </summary>
        /// <param name="element">The element.</param>
        public void AddProperty(PropertyMetaElement element)
        {
            element.ParentElement = this;
            element.Namespace = this.Namespace;

            if (this.internalProperties.ContainsKey(element.Name))
            {
                this.internalProperties[element.Name].Add(element);
            }
            else
            {
                this.internalProperties.Add(element.Name, new List<PropertyMetaElement>() { element });
            }
        }

        /// <summary>
        /// Creates the document.
        /// </summary>
        /// <returns>
        /// The HTML snippet that represents this element
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

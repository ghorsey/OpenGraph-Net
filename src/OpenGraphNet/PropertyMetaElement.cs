namespace OpenGraphNet
{
    using HtmlAgilityPack;

    /// <summary>
    /// A property meta element
    /// </summary>
    /// <seealso cref="OpenGraphNet.MetaElement" />
    public class PropertyMetaElement : MetaElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetaElement"/> class.
        /// </summary>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="ns">The ns.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public PropertyMetaElement(StructuredMetaElement parentElement, Namespace ns, string name, string value)
            : base(ns, name, value)
        {
            this.ParentElement = parentElement;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetaElement"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public PropertyMetaElement(string name, string value)
            : this(null, null, name, value)
        {
        }

        /// <summary>
        /// Gets the parent element.
        /// </summary>
        /// <value>
        /// The parent element.
        /// </value>
        public StructuredMetaElement ParentElement { get; internal set; }

        /// <summary>
        /// Creates the document.
        /// </summary>
        /// <returns>
        /// The HTML snippet that represents this element
        /// </returns>
        protected internal override HtmlDocument CreateDocument()
        {
            var doc = new HtmlDocument();

            var meta = doc.CreateElement("meta");
            meta.Attributes.Add("property", string.Concat(this.Namespace.Prefix, ":", this.ParentElement.Name, ":", this.Name));
            meta.Attributes.Add("content", this.Value);
            doc.DocumentNode.AppendChild(meta);

            return doc;
        }
    }
}

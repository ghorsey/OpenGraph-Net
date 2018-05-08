namespace OpenGraphNet
{
    using System.Collections.Generic;
    using HtmlAgilityPack;

    /// <summary>
    /// Represents an Open Graph meta element
    /// </summary>
    public sealed class MetaElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetaElement"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="name">The name.</param>
        /// <param name="values">The values.</param>
        public MetaElement(Namespace ns, string name, params string[] values) 
        {
            this.Namespace = ns;
            this.Name = name;
            this.Values = new List<string>(values);
        }

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>
        /// The namespace.
        /// </value>
        public Namespace Namespace { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public IList<string> Values { get; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="MetaElement"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(MetaElement element) 
        {
            return string.Join(", ", element.Values);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() 
        {
            var doc = new HtmlDocument();

            foreach (var itm in this.Values)
            {
                var meta = doc.CreateElement("meta");
                meta.Attributes.Add("property", string.Concat(this.Namespace.Prefix, ":", this.Name));
                meta.Attributes.Add("content", itm);
                doc.DocumentNode.AppendChild(meta);
            }

            return doc.ToString();
        }
    }
}
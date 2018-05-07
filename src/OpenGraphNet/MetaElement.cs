namespace OpenGraphNet
{
    using System.Collections.Generic;
    using HtmlAgilityPack;

    public sealed class MetaElement
    {
        public Namespace Namespace{ get; set; }

        public string Name{ get; }
        public IList<string> Values{ get; }

        public MetaElement(Namespace ns, string name, params string[] values) 
        {
            this.Namespace = ns;
            this.Name = name;
            this.Values = new List<string>(values);
        }

        public static implicit operator string(MetaElement element) 
        {
            return string.Join( ", ", this.Values);
        }

        public override string ToString() 
        {
            var doc = new HtmlDocument();

            foreach(var value in this.Values)
            {
                foreach (var itm in this.Values)
                {
                    var meta = doc.CreateElement("meta");
                    meta.Attributes.Add("property", string.Concat(this.Namespace.Prefix, ":", this.Name));
                    meta.Attributes.Add("content", itm);
                    doc.DocumentNode.AppendChild(meta);
                }
            }

            return this.ToString();
        }
    }
}
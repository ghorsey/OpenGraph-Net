namespace OpenGraphNet.SchemaRegistry
{
    using System.Collections.Generic;

    public sealed class SchemaNamespace : Namespace
    {
        public IList<string> RequiredElements{ get; set; }

        public SchemaNamespace(string prefix, string schemaUri, params string[] requiredElements) : base(prefix, schemaUri) {
            this.RequiredElements = new List<string>(requiredElements);
        }
    }
}
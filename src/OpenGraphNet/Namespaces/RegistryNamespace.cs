namespace OpenGraphNet.Namespaces
{
    using System.Collections.Generic;

    public sealed class RegistryNamespace : Namespace
    {
        public IList<string> RequiredElements{ get; set; }

        public RegistryNamespace(string prefix, string schemaUri, params string[] requiredElements) : base(prefix, schemaUri) {
            this.RequiredElements = new List<string>(requiredElements);
        }
    }
}
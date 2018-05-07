namespace OpenGraphNet.Namespaces
{
    using System.Collections.Generic;

    public sealed partial class NamespaceRegistry
    {
        private static NamespaceRegistry instance;

        private static object lck = new object();

        public IDictionary<string, RegistryNamespace> Schemas{ get; }

        public static NamespaceRegistry Instance
        { 
            get
            {
                if(instance == null)
                {
                    lock(lck)
                    {
                        if(instance == null) {
                            instance = new NamespaceRegistry();
                        }
                    }
                }

                return instance;
            }
        }

        private NamespaceRegistry() 
        {
            this.Schemas = new Dictionary<string, RegistryNamespace>();

            this.RegisterSchemas();
        }

        private void RegisterSchemas()
        {
            this.Schemas.Add("og", new RegistryNamespace("og", "http://opg.me/ns#", "title", "type", "image", "url"));;
        }
    }
}
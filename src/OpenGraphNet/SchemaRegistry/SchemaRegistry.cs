namespace OpenGraphNet.SchemaRegistry
{
    using System.Collections.Generic;

    public sealed partial class SchemaRegistry
    {
        private static SchemaRegistry instance;

        private static object lck = new object();

        public IDictionary<string, SchemaNamespace> Schemas{ get; }

        public static SchemaRegistry Instance
        { 
            get
            {
                if(instance == null)
                {
                    lock(lck)
                    {
                        if(instance == null) {
                            instance = new SchemaRegistry();
                        }
                    }
                }

                return instance;
            }
        }

        private SchemaRegistry() 
        {
            this.Schemas = new Dictionary<string, SchemaNamespace>();

            this.RegisterSchemas();
        }

        private void RegisterSchemas()
        {
            this.Schemas.Add("og", new SchemaNamespace("og", "http://opg.me/ns#", "title", "type", "image", "url"));;
        }
    }
}
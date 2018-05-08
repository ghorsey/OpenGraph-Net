namespace OpenGraphNet.Namespaces
{
    using System.Collections.Generic;

    /// <summary>
    /// A singleton to define supported namespaces
    /// </summary>
    public sealed class NamespaceRegistry
    {
        /// <summary>
        /// The synchronization lock
        /// </summary>
        private static readonly object SynchronizationLock = new object();

        /// <summary>
        /// The instance
        /// </summary>
        private static NamespaceRegistry internalInstance;

        private NamespaceRegistry() 
        {
            this.Schemas = new Dictionary<string, RegistryNamespace>();

            this.RegisterSchemas();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static NamespaceRegistry Instance
        { 
            get
            {
                if (internalInstance == null)
                {
                    lock (SynchronizationLock)
                    {
                        if (internalInstance == null)
                        {
                            internalInstance = new NamespaceRegistry();
                        }
                    }
                }

                return internalInstance;
            }
        }

        /// <summary>
        /// Gets the schemas.
        /// </summary>
        /// <value>
        /// The schemas.
        /// </value>
        public IDictionary<string, RegistryNamespace> Schemas { get; }

        private void RegisterSchemas()
        {
            this.Schemas.Add("og", new RegistryNamespace("og", "http://opg.me/ns#", "title", "type", "image", "url"));
        }
    }
}
namespace OpenGraphNet.Namespaces
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;

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
            this.InternalNamespaces = new Dictionary<string, RegistryNamespace>
                                          {
                                              { "og", new RegistryNamespace("og", "http://opg.me/ns#", "title", "type", "image", "url") },
                                              { "article", new RegistryNamespace("article", "http://ogp.me/ns/article#") },
                                              { "book", new RegistryNamespace("book", "http://ogp.me/ns/book#") },
                                              { "books", new RegistryNamespace("books", "http://ogp.me/ns/books#") },
                                              { "business", new RegistryNamespace("business", "http://ogp.me/ns/business#") },
                                              { "fitness", new RegistryNamespace("fitness", "http://ogp.me/ns/fitness#") },
                                              { "game", new RegistryNamespace("game", "http://ogp.me/ns/game#") },
                                              { "music", new RegistryNamespace("music", "http://ogp.me/ns/music#") },
                                              { "place", new RegistryNamespace("place", "http://ogp.me/ns/place#") },
                                              { "product", new RegistryNamespace("product", "http://ogp.me/ns/product#") },
                                              { "profile", new RegistryNamespace("profile", "http://ogp.me/ns/profile#") },
                                              { "restaurant", new RegistryNamespace("restaurant", "http://ogp.me/ns/restaurant#") },
                                              { "video", new RegistryNamespace("video", "http://ogp.me/ns/video#") },
                                          };
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
        /// Gets the namespaces.
        /// </summary>
        /// <value>
        /// The namespaces.
        /// </value>
        public IDictionary<string, RegistryNamespace> Namespaces => new ReadOnlyDictionary<string, RegistryNamespace>(this.InternalNamespaces);

        /// <summary>
        /// Gets the schemas.
        /// </summary>
        /// <value>
        /// The schemas.
        /// </value>
        private IDictionary<string, RegistryNamespace> InternalNamespaces { get; } = new Dictionary<string, RegistryNamespace>();
    }
}
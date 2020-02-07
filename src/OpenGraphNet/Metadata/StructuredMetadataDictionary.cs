namespace OpenGraphNet.Metadata
{
    using System.Collections;
#if NETSTANDARD2_1
    using System;
#endif
    using System.Collections.Generic;

    using OpenGraphNet.Namespaces;

    /// <summary>
    /// A collection class to contain <see cref="StructuredMetadata"/> objects.
    /// </summary>
    public class StructuredMetadataDictionary : IDictionary<string, IList<StructuredMetadata>>
    {
        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        public int Count => this.InternalCollection.Count;

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
        /// </summary>
        public bool IsReadOnly => this.InternalCollection.IsReadOnly;

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"></see> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
        /// </summary>
        public ICollection<string> Keys => this.InternalCollection.Keys;

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"></see> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
        /// </summary>
        public ICollection<IList<StructuredMetadata>> Values => this.InternalCollection.Values;

        /// <summary>
        /// Gets the internal collection.
        /// </summary>
        /// <value>
        /// The internal collection.
        /// </value>
        private IDictionary<string, IList<StructuredMetadata>> InternalCollection { get; } = new Dictionary<string, IList<StructuredMetadata>>();

        /// <summary>
        /// Gets or sets the <see cref="IList{StructuredMetadata}"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="IList{StructuredMetadata}"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns>The metadata at the current specified key.</returns>
        public IList<StructuredMetadata> this[string key]
        {
            get
            {
                var ns = NamespaceRegistry.DefaultNamespace;
#if NETSTANDARD2_1
                if (key?.IndexOf(':', System.StringComparison.OrdinalIgnoreCase) < 0)
#else
                if (key?.IndexOf(':') < 0)
#endif
                {
                    key = string.Concat(ns.Prefix, ":", key);
                }

                if (!this.InternalCollection.ContainsKey(key))
                {
                    return new List<StructuredMetadata>();
                }

                return this.InternalCollection[key];
            }

            set => this.InternalCollection[key] = value;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<string, IList<StructuredMetadata>>> GetEnumerator() => this.InternalCollection.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
#if NETSTANDARD2_1
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
#else
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
#endif

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        public void Add(KeyValuePair<string, IList<StructuredMetadata>> item) => this.InternalCollection.Add(item);

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        public void Clear() => this.InternalCollection.Clear();

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        /// true if <paramref name="item">item</paramref> is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
        /// </returns>
        public bool Contains(KeyValuePair<string, IList<StructuredMetadata>> item) => this.InternalCollection.Contains(item);

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(KeyValuePair<string, IList<StructuredMetadata>>[] array, int arrayIndex) => this.InternalCollection.CopyTo(array, arrayIndex);

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        /// true if <paramref name="item">item</paramref> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if <paramref name="item">item</paramref> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </returns>
        public bool Remove(KeyValuePair<string, IList<StructuredMetadata>> item) => this.InternalCollection.Remove(item);

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        public void Add(string key, IList<StructuredMetadata> value) => this.InternalCollection.Add(key, value);

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2"></see> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.</param>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2"></see> contains an element with the key; otherwise, false.
        /// </returns>
        public bool ContainsKey(string key) => this.InternalCollection.ContainsKey(key);

        /// <summary>
        /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key">key</paramref> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
        /// </returns>
        public bool Remove(string key) => this.InternalCollection.Remove(key);

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"></see> contains an element with the specified key; otherwise, false.
        /// </returns>
        public bool TryGetValue(string key, out IList<StructuredMetadata> value) => this.InternalCollection.TryGetValue(key, out value);
    }
}

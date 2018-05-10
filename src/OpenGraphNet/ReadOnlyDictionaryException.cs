namespace OpenGraphNet
{
    using System;

    /// <summary>
    /// Read-only dictionary exception
    /// </summary>
    /// <seealso cref="System.NotSupportedException" />
    [Serializable]
    public class ReadOnlyDictionaryException : NotSupportedException
    {
    }
}

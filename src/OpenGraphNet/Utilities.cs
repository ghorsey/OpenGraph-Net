namespace OpenGraphNet;

using System.Reflection;

/// <summary>
/// Class Utilities.
/// </summary>
internal static class Utilities
{
    /// <summary>
    /// Cctors this instance.
    /// </summary>
    /// <returns>Information version.</returns>
    internal static string GetVersionString()
    {
        return typeof(Utilities).Assembly.GetCustomAttribute<System.Reflection.AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}

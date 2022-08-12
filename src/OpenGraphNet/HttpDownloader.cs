namespace OpenGraphNet;

/// <summary>
/// HTTP Downloader.
/// </summary>
/// <remarks>
/// Taken from http://stackoverflow.com/a/2700707.
/// </remarks>
[ExcludeFromCodeCoverage]
public class HttpDownloader
{
    /// <summary>
    /// The referrer.
    /// </summary>
    private readonly string? referrer;

    /// <summary>
    /// The user agent.
    /// </summary>
    private readonly string userAgent;

    /// <summary>
    /// The timeout in milliseconds.
    /// </summary>
    private readonly int timeout;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpDownloader" /> class.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="referrer">The referrer.</param>
    /// <param name="userAgent">The user agent.</param>
    /// <param name="timeout">The timeout in milliseconds.</param>
    public HttpDownloader(Uri url, string? referrer, string userAgent, int timeout)
    {
        this.Url = url;
        this.userAgent = userAgent;
        this.referrer = referrer;
        this.timeout = timeout;
    }

    /// <summary>
    /// Gets or sets the URL.
    /// </summary>
    /// <value>
    /// The URL.
    /// </value>
    public Uri Url { get; set; }

    /// <summary>
    /// Gets the page asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The content of the page.</returns>
    public async Task<string> GetPageAsync(CancellationToken cancellationToken = default)
    {
        using var handler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
        };

        using HttpClient client = new(handler);
        client.Timeout = TimeSpan.FromMilliseconds(this.timeout);

        if (!string.IsNullOrWhiteSpace(this.userAgent))
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd(this.userAgent);
        }

        if (!string.IsNullOrWhiteSpace(this.referrer))
        {
            client.DefaultRequestHeaders.Referrer = new Uri(this.referrer);
        }

        var response = await client.GetAsync(this.Url, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
#if NETSTANDARD2_1_OR_GREATER
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#else
        return await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#endif
    }
}

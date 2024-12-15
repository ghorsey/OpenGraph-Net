namespace OpenGraphNet;

using System.Net.Http.Headers;

using System.Net.Mime;

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
    /// Default Http Handler, configured to support compression.
    /// </summary>
    private static readonly HttpClientHandler HttpClientHandler = new()
    {
#if NET6_OR_GREATER
        AutomaticDecompression = DecompressionMethods.All,
#else
        AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
#endif
    };

    /// <summary>
    /// Default HttpClient, used when one is not specified during construction.
    /// Ideally this would be static to pool between all instances, but as you can set different referrers, user agents and timeouts
    /// when creating an HttpDownloader this isn't possible.
    /// </summary>
    private readonly HttpClient defaultHttpClient = new(HttpClientHandler, disposeHandler: true);

    /// <summary>
    /// Initializes static members of the <see cref="HttpDownloader"/> class.
    /// </summary>
    static HttpDownloader()
    {
        // Configure the shared client with opinionated defaults.
#if NET6_OR_GREATER
        SharedHttpClient.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;
        SharedHttpClient.DefaultRequestVersion = HttpVersion.Version20;
#endif
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpDownloader" /> class.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="referrer">The referrer.</param>
    /// <param name="userAgent">The user agent.</param>
    /// <param name="timeout">The timeout in milliseconds.</param>
    /// <param name="httpClient">An optional <see cref="HttpClient"/> to use to download.</param>
    public HttpDownloader(Uri url, string? referrer, string userAgent, int timeout, HttpClient? httpClient = null)
    {
        this.Url = url;

        this.HttpClient = httpClient ?? this.defaultHttpClient;

        if (httpClient is null)
        {
            this.HttpClient.Timeout = TimeSpan.FromMilliseconds(timeout);

            if (!string.IsNullOrEmpty(userAgent))
            {
                this.HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
            }

            if (!string.IsNullOrEmpty(referrer))
            {
                this.HttpClient.DefaultRequestHeaders.Referrer = new Uri(referrer);
            }
        }
    }

    /// <summary>
    /// Gets or sets the URL.
    /// </summary>
    /// <value>
    /// The URL.
    /// </value>
    public Uri Url { get; set; }

    /// <summary>
    /// Gets the HttpClient to use when making requests.
    /// </summary>
    protected HttpClient HttpClient { get; }

    /// <summary>
    /// Gets the page asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The content of the page.</returns>
    public async Task<string> GetPageAsync(CancellationToken cancellationToken = default)
    {
        var response = await this.HttpClient.GetAsync(this.Url, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
#if NETSTANDARD2_1_OR_GREATER
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#else
        return await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#endif
    }
}

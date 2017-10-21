using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenGraph_Net
{
    /// <summary>
    /// Http Downloader
    /// </summary>
    public class HttpDownloader
    {
        private readonly Uri _url;
        private readonly string _referer;
        private readonly string _userAgent;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpDownloader"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="referer">The referer.</param>
        /// <param name="userAgent">The user agent.</param>
        public HttpDownloader(Uri url, string referer, string userAgent)
        {
            _url = url;
            _referer = referer;
            _userAgent = userAgent;

            _httpClient = new HttpClient();

            if (!string.IsNullOrEmpty(referer))
            {
                _httpClient.DefaultRequestHeaders.Add("Referer", _referer);
            }

            if (!string.IsNullOrEmpty(userAgent))
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", _userAgent);
            }
        }

        /// <summary>
        /// Gets the page asynchronosly
        /// </summary>
        /// <returns>
        /// The content of the page
        /// </returns>
        public async Task<string> GetPageAsync()
        {
            var result = await _httpClient.GetStringAsync(_url);

            return result;
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <returns>The content of the page</returns>
        public string GetPage()
        {
            return GetPageAsync().Result;
        }
    }
}

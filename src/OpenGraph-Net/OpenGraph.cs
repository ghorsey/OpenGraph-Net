using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Globalization;
using System.Web;
using System.Net;
using System.IO;

namespace OpenGraph_Net
{
    /// <summary>
    /// Represents Open Graph meta data parsed from HTML
    /// </summary>
    public class OpenGraph : IDictionary<string, string>
    {
        private static readonly string[] RequiredMeta = new string[] { "title", "type", "image", "url" };

        private static readonly string[] BaseTypes = new string[] 
        {
            // activities
            "activity", "sport",
            // business
            "bar", "company", "cafe", "hotel", "restaurant",
            //groups
            "cause", "sports_league", "sports_team",
            // organizations
            "band", "government", "non_profit", "school", "university",
            // people
            "actor", "athelete", "author", "director", "musician", "politician", "profile", "public_figure",
            //places
            "city", "country", "landmark", "state_province",
            // products
            "album", "book", "drink", "food", "game", "movie", "product", "song", "tv_show",
            //website
            "article", "blog", "website"

        };

        private IDictionary<string, string> OpenGraphData;
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type of open graph document.</value>
        public string Type { get; private set; }
        /// <summary>
        /// Gets the title of the open graph document.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; private set; }
        /// <summary>
        /// Gets the image for the open graph document.
        /// </summary>
        /// <value>The image.</value>
        public Uri Image { get; private set; }
        /// <summary>
        /// Gets the URL for the open graph docuemnt
        /// </summary>
        /// <value>The URL.</value>
        public Uri Url { get; private set; }

        /// <summary>
        /// Gets the original URL used to generate this graph
        /// </summary>
        /// <value>The original URL.</value>
        public Uri OriginalUrl { get; private set; } 

        private OpenGraph()
        {
            OpenGraphData = new Dictionary<string, string>();
        }


        /// <summary>
        /// Downloads the HTML of the specified URL and parses it for open graph content.
        /// </summary>
        /// <param name="url">The URL to download the HTML from.</param>
        /// <param name="userAgent">The user agent to use when downloading content.  The default is 'facebookexternalhit' which is required for some site (like amazon) to includ open graph data.</param>
        /// <param name="validateSpecification">if set to <c>true</c> verify that the document meets the required attributes of the open graph specification.</param>
        /// <returns></returns>
        public static OpenGraph ParseUrl(string url, string userAgent = "facebookexternalhit", bool validateSpecifiction = false)
        {
            Uri uri = new Uri(url);
            return ParseUrl(uri, userAgent, validateSpecifiction);
        }
        /// <summary>
        /// Downloads the HTML of the specified URL and parses it for open graph content.
        /// </summary>
        /// <param name="url">The URL to download the HTML from.</param>
        /// <param name="userAgent">The user agent to use when downloading content.  The default is 'facebookexternalhit' which is required for some site (like amazon) to includ open graph data.</param>
        /// <param name="validateSpecification">if set to <c>true</c> verify that the document meets the required attributes of the open graph specification.</param>
        /// <returns></returns>
        public static OpenGraph ParseUrl(Uri url, string userAgent = "facebookexternalhit", bool validateSpecification = false)
        {
            OpenGraph result = new OpenGraph();

            result.OriginalUrl = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = userAgent;


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string html = "";
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                html = reader.ReadToEnd();
            }


            return ParseHtml(result, html, validateSpecification);
        }

        /// <summary>
        /// Parses the HTML for open graph content.
        /// </summary>
        /// <param name="content">The HTML to parse.</param>
        /// <param name="validateSpecification">if set to <c>true</c> verify that the document meets the required attributes of the open graph specification.</param>
        /// <returns></returns>
        public static OpenGraph ParseHtml(string content, bool validateSpecification = false)
        {
            OpenGraph result = new OpenGraph();
            return ParseHtml(result, content, validateSpecification);
        }
        
        private static OpenGraph ParseHtml(OpenGraph result, string content, bool validateSpecification = false)
        {
         

            int indexOfClosingHead = content.IndexOf("</head>");
            string toParse = content.Substring(0, indexOfClosingHead + 7);

            toParse = toParse + "<body></body></html>\r\n";

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(toParse);
            
            document.LoadHtml(toParse);
            
            HtmlNodeCollection allMeta = document.DocumentNode.SelectNodes("//meta");

            var ogMetaTags = from meta in allMeta
                             where (meta.Attributes.Contains("property") && meta.Attributes["property"].Value.StartsWith("og:")) ||
                             (meta.Attributes.Contains("name") && meta.Attributes["name"].Value.StartsWith("og:"))
                             select meta;

            foreach (HtmlNode metaTag in ogMetaTags)
            {
                string value = GetOpenGraphValue(metaTag);
                if (string.IsNullOrWhiteSpace(value)) continue;

                result.OpenGraphData.Add(GetOpenGraphKey(metaTag), GetOpenGraphValue(metaTag));
            }

            string type = "";
            result.OpenGraphData.TryGetValue("type", out type);
            result.Type = type ?? string.Empty;

            string title = "";
            result.OpenGraphData.TryGetValue("title", out title);
            result.Title = title ?? string.Empty;

            try
            {
                string image = "";
                result.OpenGraphData.TryGetValue("image", out image);
                result.Image = new Uri(image);
            }
            catch (UriFormatException)
            {
                // do nothing
            }
            catch (ArgumentException)
            {
                // do nothing
            }

            try
            {

                string url = "";
                result.OpenGraphData.TryGetValue("url", out url);
                result.Url = new Uri(url);
            }
            catch (UriFormatException)
            {
                // do nothing
            }
            catch (ArgumentException)
            {
                // do nothing
            }

            if( validateSpecification )
                foreach (string required in RequiredMeta)
                {
                    if (!result.ContainsKey(required))
                        throw new InvalidSpecificationException("The parsed HTML does not meet the open graph specification");
                }


            return result;
        }

        private static string GetOpenGraphKey(HtmlNode metaTag)
        {
            if (metaTag.Attributes.Contains("property"))
                return CleanOpenGraphKey(metaTag.Attributes["property"].Value);
            else
                return CleanOpenGraphKey(metaTag.Attributes["name"].Value);
        }

        private static string CleanOpenGraphKey(string value)
        {
            return value.Replace("og:", "").ToLower(CultureInfo.InvariantCulture);
        }

        private static string GetOpenGraphValue(HtmlNode metaTag)
        {
            if (!metaTag.Attributes.Contains("content")) return "";

            return metaTag.Attributes["content"].Value;
        }


        #region IDictionary<string,string> Members

        public void Add(string key, string value)
        {
            throw new ReadOnlyDictionaryException();
        }

        public bool ContainsKey(string key)
        {
            return OpenGraphData.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return OpenGraphData.Keys; }
        }

        public bool Remove(string key)
        {
            throw new ReadOnlyDictionaryException();
        }

        public bool TryGetValue(string key, out string value)
        {
            return OpenGraphData.TryGetValue(key, out value);
        }

        public ICollection<string> Values
        {
            get { return OpenGraphData.Values; }
        }

        public string this[string key]
        {
            get
            {
                if (!OpenGraphData.ContainsKey(key)) return string.Empty;
                return OpenGraphData[key];
            }
            set
            {
                throw new ReadOnlyDictionaryException();
            }
        }

        #endregion
        
        #region ICollection<KeyValuePair<string,string>> Members

        public void Add(KeyValuePair<string, string> item)
        {
            throw new ReadOnlyDictionaryException();
        }

        public void Clear()
        {
            throw new ReadOnlyDictionaryException();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return OpenGraphData.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            OpenGraphData.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return OpenGraphData.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            throw new ReadOnlyDictionaryException();
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,string>> Members

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return OpenGraphData.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)OpenGraphData).GetEnumerator();
        }

        #endregion     
    }

 
}

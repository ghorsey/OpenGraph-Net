using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;

namespace OpenGraph_Net
{
    public class OpenGraphMetaTagParser : IParsingStrategy
    {
        public void Parse(IDictionary<string, string> openGraphData, HtmlDocument document)
        {
            HtmlNodeCollection allMeta = document.DocumentNode.SelectNodes("//meta");

            var openGraphMetaTags = from meta in allMeta ?? new HtmlNodeCollection(null)
                where (meta.Attributes.Contains("property") && meta.Attributes["property"].Value.StartsWith("og:")) ||
                      (meta.Attributes.Contains("name") && meta.Attributes["name"].Value.StartsWith("og:"))
                select meta;

            foreach (HtmlNode metaTag in openGraphMetaTags)
            {
                string value = GetOpenGraphValue(metaTag);
                string property = GetOpenGraphKey(metaTag);
                if (String.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                if (openGraphData.ContainsKey(property))
                {
                    continue;
                }

                openGraphData.Add(property, value);
            }

        }


        /// <summary>
        /// Gets the open graph key.
        /// </summary>
        /// <param name="metaTag">The meta tag.</param>
        /// <returns>Returns the key stored from the meta tag</returns>
        private static string GetOpenGraphKey(HtmlNode metaTag)
        {
            if (metaTag.Attributes.Contains("property"))
            {
                return CleanOpenGraphKey(metaTag.Attributes["property"].Value);
            }
            else
            {
                return CleanOpenGraphKey(metaTag.Attributes["name"].Value);
            }
        }


        /// <summary>
        /// Cleans the open graph key.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>strips the <c>og:</c> namespace from the value</returns>
        private static string CleanOpenGraphKey(string value)
        {
            return value.Replace("og:", string.Empty).ToLower(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets the open graph value.
        /// </summary>
        /// <param name="metaTag">The meta tag.</param>
        /// <returns>Returns the value from the meta tag</returns>
        private static string GetOpenGraphValue(HtmlNode metaTag)
        {
            if (!metaTag.Attributes.Contains("content"))
            {
                return string.Empty;
            }

            return metaTag.Attributes["content"].Value;
        }
    }
}
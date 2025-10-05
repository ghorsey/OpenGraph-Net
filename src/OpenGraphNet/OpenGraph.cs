﻿namespace OpenGraphNet;

/// <summary>
/// Represents Open Graph meta data parsed from HTML.
/// </summary>
public class OpenGraph
{
    /// <summary>
    /// The open graph data.
    /// </summary>
    private readonly StructuredMetadataDictionary internalOpenGraphData;

    /// <summary>
    /// Prevents a default instance of the <see cref="OpenGraph" /> class from being created.
    /// </summary>
    private OpenGraph()
    {
        this.internalOpenGraphData = new StructuredMetadataDictionary();
        this.Namespaces = new Dictionary<string, OpenGraphNamespace>();
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public IDictionary<string, IList<StructuredMetadata>> Metadata => new ReadOnlyDictionary<string, IList<StructuredMetadata>>(this.internalOpenGraphData);

    /// <summary>
    /// Gets the namespaces.
    /// </summary>
    /// <value>The namespaces.</value>
    public IDictionary<string, OpenGraphNamespace> Namespaces { get; }

    /// <summary>
    /// Gets the type.
    /// </summary>
    /// <value>The type of open graph document.</value>
    public string Type { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the title of the open graph document.
    /// </summary>
    /// <value>The title.</value>
    public string Title { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the image for the open graph document.
    /// </summary>
    /// <value>The image.</value>
    public Uri? Image { get; private set; }

    /// <summary>
    /// Gets the URL for the open graph document.
    /// </summary>
    /// <value>The URL.</value>
    public Uri? Url { get; private set; }

    /// <summary>
    /// Gets the original URL used to generate this graph.
    /// </summary>
    /// <value>The original URL.</value>
    public Uri? OriginalUrl { get; private set; }

    /// <summary>
    /// Gets the original HTML content.
    /// </summary>
    /// <value>
    /// The original HTML content.
    /// </value>
    public string OriginalHtml { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the head prefix attribute value.
    /// </summary>
    /// <value>
    /// The head prefix attribute value.
    /// </value>
    public string HeadPrefixAttributeValue
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var ns in this.Namespaces)
            {
                _ = sb.AppendFormat(CultureInfo.InvariantCulture, " {0}", ns.Value);
            }

            return sb.ToString().Trim();
        }
    }

    /// <summary>
    /// Gets the HTML XML namespace values.
    /// </summary>
    /// <value>
    /// The HTML XML namespace values.
    /// </value>
    public string HtmlXmlnsValues
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var ns in this.Namespaces)
            {
                _ = sb.AppendFormat(CultureInfo.InvariantCulture, " xmlns:{0}=\"{1}\"", ns.Value.Prefix, ns.Value.SchemaUri);
            }

            return sb.ToString().Trim();
        }
    }

    /// <summary>
    /// Makes the graph.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="type">The type.</param>
    /// <param name="image">The image.</param>
    /// <param name="url">The URL.</param>
    /// <param name="description">The description.</param>
    /// <param name="siteName">Name of the site.</param>
    /// <param name="audio">The audio.</param>
    /// <param name="video">The video.</param>
    /// <param name="locale">The locale.</param>
    /// <param name="localeAlternates">The locale alternates.</param>
    /// <param name="determiner">The determiner.</param>
    /// <returns><see cref="OpenGraph"/>.</returns>
    public static OpenGraph MakeGraph(
        string title,
        string type,
        string image,
        string url,
        string description = "",
        string siteName = "",
        string audio = "",
        string video = "",
        string locale = "",
        IList<string>? localeAlternates = null,
        string determiner = "")
    {
        var graph = new OpenGraph
        {
            Title = title,
            Type = type,
            Image = new Uri(image, UriKind.Absolute),
            Url = new Uri(url, UriKind.Absolute),
        };
        var ns = NamespaceRegistry.Instance.Namespaces["og"];

        graph.Namespaces.Add(ns.Prefix, ns);
        graph.AddMetadata(new StructuredMetadata(ns, "title", title));
        graph.AddMetadata(new StructuredMetadata(ns, "type", type));
        graph.AddMetadata(new StructuredMetadata(ns, "image", image));
        graph.AddMetadata(new StructuredMetadata(ns, "url", url));

        if (!string.IsNullOrWhiteSpace(description))
        {
            graph.AddMetadata(new StructuredMetadata(ns, "description", description));
        }

        if (!string.IsNullOrWhiteSpace(siteName))
        {
            graph.AddMetadata(new StructuredMetadata(ns, "site_name", siteName));
        }

        if (!string.IsNullOrWhiteSpace(audio))
        {
            graph.AddMetadata(new StructuredMetadata(ns, "audio", audio));
        }

        if (!string.IsNullOrWhiteSpace(video))
        {
            graph.AddMetadata(new StructuredMetadata(ns, "video", video));
        }

        if (!string.IsNullOrWhiteSpace(locale))
        {
            graph.AddMetadata(new StructuredMetadata(ns, "locale", locale));
        }

        if (!string.IsNullOrWhiteSpace(determiner))
        {
            graph.AddMetadata(new StructuredMetadata(ns, "determiner", determiner));
        }

        if (graph.internalOpenGraphData.ContainsKey("og:locale"))
        {
            var localeElement = graph.internalOpenGraphData["og:locale"].First();
            foreach (var localeAlternate in localeAlternates ?? new List<string>())
            {
                localeElement.AddProperty("alternate", localeAlternate);
            }
        }
        else
        {
            foreach (var localeAlternate in localeAlternates ?? new List<string>())
            {
                graph.AddMetadata(new StructuredMetadata(ns, "locale:alternate", localeAlternate));
            }
        }

        return graph;
    }

    /// <summary>
    /// Parses the URL asynchronously.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="userAgent">The user agent.</param>
    /// <param name="validateSpecification">if set to <c>true</c> validate minimum Open Graph specification.</param>
    /// <param name="timeout">The timeout in milliseconds.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns><see cref="Task{OpenGraph}" />.</returns>
    public static Task<OpenGraph> ParseUrlAsync(string url, string? userAgent = null, bool validateSpecification = false, int timeout = 90000, CancellationToken cancellationToken = default)
    {
        if (!Regex.IsMatch(url, "^https?://", RegexOptions.IgnoreCase))
        {
            url = $"http://{url}";
        }

        userAgent ??= $"OpenGraphNet/{Utilities.GetVersionString()}";

        Uri uri = new(url);
        return ParseUrlAsync(uri, userAgent, validateSpecification, timeout, cancellationToken);
    }

    /// <summary>
    /// Parses the URL asynchronously.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="userAgent">The user agent.</param>
    /// <param name="validateSpecification">if set to <c>true</c> [validate specification].</param>
    /// <param name="timeout">The timeout in milliseconds.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns><see cref="Task{OpenGraph}" />.</returns>
    public static async Task<OpenGraph> ParseUrlAsync(Uri url, string userAgent = "", bool validateSpecification = false, int timeout = 90000, CancellationToken cancellationToken = default)
    {
        OpenGraph result = new() { OriginalUrl = url };

        HttpDownloader downloader = new(url, null, userAgent, timeout);
        string html = await downloader.GetPageAsync(cancellationToken).ConfigureAwait(false);
        result.OriginalHtml = html;

        return ParseHtml(result, html, validateSpecification);
    }

    /// <summary>
    /// Parses the HTML for open graph content.
    /// </summary>
    /// <param name="content">The HTML to parse.</param>
    /// <param name="validateSpecification">if set to <c>true</c> verify that the document meets the required attributes of the open graph specification.</param>
    /// <returns><see cref="OpenGraph"/>.</returns>
    public static OpenGraph ParseHtml(string content, bool validateSpecification = false)
    {
        OpenGraph result = new();
        return ParseHtml(result, content, validateSpecification);
    }

    /// <summary>
    /// Adds the meta element.
    /// </summary>
    /// <param name="element">The element.</param>
    public void AddMetadata(StructuredMetadata element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        var key = string.Concat(element.Namespace.Prefix, ":", element.Name);
        if (this.internalOpenGraphData.ContainsKey(key))
        {
            this.internalOpenGraphData[key].Add(element);
        }
        else
        {
            this.internalOpenGraphData.Add(key, new List<StructuredMetadata> { element });
        }
    }

    /// <summary>
    /// Adds the metadata.
    /// </summary>
    /// <param name="prefix">The prefix.</param>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="InvalidOperationException">The prefix {prefix} does not exist in the NamespaceRegistry.</exception>
    public void AddMetadata(string prefix, string name, string value)
    {
        if (!NamespaceRegistry.Instance.Namespaces.ContainsKey(prefix))
        {
            throw new InvalidOperationException($"The prefix {prefix} does not exist in the {nameof(NamespaceRegistry)}");
        }

        var ns = NamespaceRegistry.Instance.Namespaces[prefix];

        var metadata = new StructuredMetadata(ns, name, value);
        this.AddMetadata(metadata);
    }

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        var doc = new HtmlDocument();

        var elements = this.internalOpenGraphData.SelectMany(og => og.Value).ToList();

        foreach (var structuredMetaElement in elements)
        {
            doc.DocumentNode.AppendChild(structuredMetaElement.CreateDocument().DocumentNode);
        }

        return doc.DocumentNode.InnerHtml;
    }

    /// <summary>
    /// Safes the HTML decode URL.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The string.</returns>
    private static string HtmlDecodeUrl(string value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        // naive attempt
        var patterns = new Dictionary<string, string>
        {
            ["&amp;"] = "&",
        };

        foreach (var key in patterns)
        {
            value = value.Replace(key.Key, key.Value);
        }

        return value;
    }

    /// <summary>
    /// Gets the open graph key.
    /// </summary>
    /// <param name="metaTag">The meta tag.</param>
    /// <returns>Returns the key stored from the meta tag.</returns>
    private static string GetOpenGraphKey(HtmlNode metaTag)
    {
        if (metaTag.Attributes.Contains("property"))
        {
            return metaTag.Attributes["property"].Value;
        }

        return metaTag.Attributes["name"].Value;
    }

    /// <summary>
    /// Gets the open graph prefix.
    /// </summary>
    /// <param name="metaTag">The meta tag.</param>
    /// <returns>The prefix.</returns>
    private static string GetOpenGraphPrefix(HtmlNode metaTag)
    {
        var value = metaTag.Attributes.Contains("property") ? metaTag.Attributes["property"].Value : metaTag.Attributes["name"].Value;

        return value.Split(':')[0];
    }

    /// <summary>
    /// Cleans the open graph key.
    /// </summary>
    /// <param name="prefix">The prefix.</param>
    /// <param name="value">The value.</param>
    /// <returns>
    /// strips the namespace prefix from the value.
    /// </returns>
    private static string CleanOpenGraphKey(string prefix, string value)
    {
        return value.Replace(string.Concat(prefix, ":"), string.Empty).ToLower(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Gets the open graph value.
    /// </summary>
    /// <param name="metaTag">The meta tag.</param>
    /// <returns>Returns the value from the meta tag.</returns>
    private static string GetOpenGraphValue(HtmlNode metaTag)
    {
        if (!metaTag.Attributes.Contains("content"))
        {
            return string.Empty;
        }

        return metaTag.Attributes["content"].Value;
    }

    /// <summary>
    /// Initializes the <see cref="OpenGraph" /> class.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="document">The document.</param>
    private static void ParseNamespaces(OpenGraph result, HtmlDocument document)
    {
        const string NamespacePattern = @"(\w+):\s?(https?://[^\s]+)";

        HtmlNode head = document.DocumentNode.SelectSingleNode("//head");
        HtmlNode html = document.DocumentNode.SelectSingleNode("html");

        if (head != null && head.Attributes.Contains("prefix") && Regex.IsMatch(head.Attributes["prefix"].Value, NamespacePattern))
        {
            var matches = Regex.Matches(
                head.Attributes["prefix"].Value,
                NamespacePattern,
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

            foreach (Match match in matches.Cast<Match>())
            {
                var prefix = match.Groups[1].Value;
                if (NamespaceRegistry.Instance.Namespaces.ContainsKey(prefix))
                {
                    result.Namespaces.Add(prefix, NamespaceRegistry.Instance.Namespaces[prefix]);
                    continue;
                }

                var ns = match.Groups[2].Value;
                result.Namespaces.Add(prefix, new OpenGraphNamespace(prefix, ns));
            }
        }
        else if (html != null && html.Attributes.Any(a => a.Name.StartsWith("xmlns:", StringComparison.InvariantCultureIgnoreCase)))
        {
            var namespaces = html.Attributes.Where(a => a.Name.StartsWith("xmlns:", StringComparison.InvariantCultureIgnoreCase));
            foreach (var ns in namespaces)
            {
                var prefix = ns.Name.ToLowerInvariant().Replace("xmlns:", string.Empty);
                result.Namespaces.Add(prefix, new OpenGraphNamespace(prefix, ns.Value));
            }
        }
        else
        {
            // append the minimum og: prefix and namespace
            result.Namespaces.Add("og", NamespaceRegistry.Instance.Namespaces["og"]);
        }
    }

    /// <summary>
    /// Matches the namespace predicate.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns><c>true</c> when the element has a namespace; otherwise <c>false</c>.</returns>
    private static bool MatchesNamespacePredicate(string value)
    {
        return value.Contains(':', StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Sets the namespace.
    /// </summary>
    /// <param name="graph">The graph.</param>
    /// <param name="prefix">The prefix.</param>
    private static void SetNamespace(OpenGraph graph, string prefix)
    {
        if (graph.Namespaces.Any(n => n.Key.Equals(prefix, StringComparison.InvariantCultureIgnoreCase)))
        {
            return;
        }

        if (NamespaceRegistry.Instance.Namespaces.Any(ns => ns.Key.Equals(prefix, StringComparison.CurrentCultureIgnoreCase)))
        {
            var ns = NamespaceRegistry.Instance.Namespaces.First(ns2 => ns2.Key.Equals(prefix, StringComparison.InvariantCultureIgnoreCase));
            graph.Namespaces.Add(ns.Key, ns.Value);
        }
    }

    /// <summary>
    /// Parses the HTML.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="content">The content.</param>
    /// <param name="validateSpecification">if set to <c>true</c> [validate specification].</param>
    /// <returns><see cref="OpenGraph"/>.</returns>
    /// <exception cref="OpenGraphNet.InvalidSpecificationException">The parsed HTML does not meet the open graph specification.</exception>
    private static OpenGraph ParseHtml(OpenGraph result, string content, bool validateSpecification = false)
    {
        HtmlDocument document = new();
        document.LoadHtml(content);

        ParseNamespaces(result, document);

        HtmlNodeCollection allMeta = document.DocumentNode.SelectNodes("//meta");

        if (allMeta == null && !validateSpecification)
        {
            return result;
        }

        if (allMeta == null && validateSpecification)
        {
            ValidateSpecification(result);
        }

        var openGraphMetaTags = from meta in allMeta
                                where (meta.Attributes.Contains("property") && MatchesNamespacePredicate(meta.Attributes["property"].Value)) ||
                                (meta.Attributes.Contains("name") && MatchesNamespacePredicate(meta.Attributes["name"].Value))
                                select meta;

        StructuredMetadata? lastElement = null;
        foreach (HtmlNode metaTag in openGraphMetaTags)
        {
            var prefix = GetOpenGraphPrefix(metaTag);
            SetNamespace(result, prefix);
            if (!result.Namespaces.ContainsKey(prefix))
            {
                continue;
            }

            string value = GetOpenGraphValue(metaTag);
            string property = GetOpenGraphKey(metaTag);
            var cleanProperty = CleanOpenGraphKey(prefix, property);

            value = HtmlDecodeUrl(property, value);

            if (lastElement != null && lastElement.IsMyProperty(property))
            {
                lastElement.AddProperty(cleanProperty, value);
            }
            else if (IsChildOfExistingElement(result.internalOpenGraphData, property))
            {
                var matchingElement =
                    result.internalOpenGraphData.First(kvp => kvp.Value.First().IsMyProperty(property));

                var element = matchingElement.Value.FirstOrDefault(e => !e.Properties.ContainsKey(cleanProperty));
                element?.AddProperty(cleanProperty, value);
            }
            else
            {
                lastElement = new StructuredMetadata(result.Namespaces[prefix], cleanProperty, value);
                result.AddMetadata(lastElement);
            }
        }

        result.Type = string.Empty;
        if (result.internalOpenGraphData.TryGetValue("og:type", out var type))
        {
            result.Type = (type.FirstOrDefault() ?? new NullMetadata()).Value ?? string.Empty;
        }

        result.Title = string.Empty;
        if (result.internalOpenGraphData.TryGetValue("og:title", out var title))
        {
            result.Title = (title.FirstOrDefault() ?? new NullMetadata()).Value ?? string.Empty;
        }

        result.Image = GetUri(result, "og:image");
        result.Url = GetUri(result, "og:url");

        if (validateSpecification)
        {
            ValidateSpecification(result);
        }

        return result;
    }

    /// <summary>
    /// Determines whether [is child of existing element] [the specified property].
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <param name="property">The property.</param>
    /// <returns>
    ///   <c>true</c> if this child of existing element] [the specified property]; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsChildOfExistingElement(StructuredMetadataDictionary collection, string property)
    {
        return collection.Any(kvp => kvp.Value.FirstOrDefault()?.IsMyProperty(property) ?? false);
    }

    /// <summary>
    /// Gets the URI.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="property">The property.</param>
    /// <returns>The Uri.</returns>
    private static Uri? GetUri(OpenGraph result, string property)
    {
        result.internalOpenGraphData.TryGetValue(property, out var url);

        try
        {
            return new Uri(url?.FirstOrDefault()?.Value ?? string.Empty);
        }
        catch (ArgumentException)
        {
            return null;
        }
        catch (UriFormatException)
        {
            return null;
        }
    }

    /// <summary>
    /// HTMLs the decode URLs.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="value">The value.</param>
    /// <returns>The decoded URL value.</returns>
    private static string HtmlDecodeUrl(string property, string value)
    {
        var urlPropertyPatterns = new[] { "image", "url^" };
        foreach (var urlPropertyPattern in urlPropertyPatterns)
        {
            if (Regex.IsMatch(property, urlPropertyPattern))
            {
                return HtmlDecodeUrl(value);
            }
        }

        return value;
    }

    /// <summary>
    /// Validates the specification.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <exception cref="InvalidSpecificationException">The parsed HTML does not meet the open graph specification, missing element: {required}.</exception>
    private static void ValidateSpecification(OpenGraph result)
    {
        var prefixes = result.Namespaces.Select(ns => ns.Value.Prefix);

        var namespaces = NamespaceRegistry
            .Instance
            .Namespaces
            .Where(ns => prefixes.Contains(ns.Key) && ns.Value.RequiredElements.Count > 0)
            .Select(ns => ns.Value)
            .ToList();

        foreach (var ns in namespaces)
        {
            foreach (var required in ns.RequiredElements)
            {
                if (!result.Metadata.ContainsKey(string.Concat(ns.Prefix, ":", required)))
                {
                    throw new InvalidSpecificationException($"The parsed HTML does not meet the open graph specification, missing element: {required}");
                }
            }
        }
    }
}

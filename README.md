# OpenGraphNet

[![Build](https://github.com/ghorsey/OpenGraph-Net/actions/workflows/main.yml/badge.svg)](https://github.com/ghorsey/OpenGraph-Net/actions/workflows/main.yml)
[![Nuget V](https://img.shields.io/nuget/v/OpenGraph-Net.svg)](http://www.nuget.org/packages/OpenGraph-Net/)
[![Nuget dl](https://img.shields.io/nuget/dt/OpenGraph-Net.svg)](http://www.nuget.org/packages/OpenGraph-Net/)
[![License](https://img.shields.io/badge/license-MIT-orange.svg)](https://raw.githubusercontent.com/ghorsey/OpenGraph-Net/master/LICENSE)
[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-2.1-4baaaa.svg)](code_of_conduct.md)

A simple .net assembly to use to parse Open Graph information from either a URL or an HTML snippet. You can read more about the
Open Graph protocol @ <http://ogp.me>.

## Support the library

If you find this library useful, [buy me a coffee](https://www.buymeacoffee.com/ghorsey)!

## Usage

These are the basic operations of the OpenGraphNet parser.

### Parsing from a URL

Synchronously parse a URL:

```csharp
OpenGraph graph = OpenGraph.ParseUrl("https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe/playlist/2lzTTRqhYS6AkHPIvdX9u3?si=KcZxfwiIR7OBPCzj20utaQ");
```

Use `async/await` to parse a URL:

```csharp
OpenGraph graph = await OpenGraph.ParseUrlAsync("https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe/playlist/2lzTTRqhYS6AkHPIvdX9u3?si=KcZxfwiIR7OBPCzj20utaQ");
```

### Accessing Values

#### Accessing Metadata

Each metadata element is stored as an array. Additionally, each element's properties are also stored as an array.

```html
<meta property="og:image" content="http://example.com/img1.png">
<meta property="og:image:width" content="30">
<meta property="og:image" content="http://example.com/img2.png">
<meta property="og:image:width" content="60">
<meta property="og:locale" content="en">
<meta property="og:locale:alternate" content="en_US">
<meta property="og:locale:alternate" content="en_GB">
```

You would access the values from the sample HTML above as:

```csharp
graph.Metadata["og:image"].First().Value; // "http://example.com/img1.png"
graph.Metadata["og:image"].First().Properties["width"].Value(); // "30"
graph.Metadata["og:image"][1].Value; // "http://example.com/img2.png"
graph.Metadata["og:image"][1].Properties["width"].Value(); // "30"
graph.Metadata["og:locale"].Value(); // "en"
graph.Metadata["og:locale"].First().Properties["alternate"][0].Value; // "en_US"
graph.Metadata["og:locale"].First().Properties["alternate"][1].Value; // "en_GB"
```

#### Basic Metadata

The four required Open Graph properties for all pages are available as direct properties on the OpenGraph object.

* `graph.Type` is a shortcut for `graph.Metadata["og:type"].Value()`
* `graph.Title` is a shortcut for `graph.Metadata["og:title"].Value()`
* `graph.Image` is a shortcut for `graph.Metadata["og:image"].Value()`
* Note: since there can be multiple images, this helper returns the URI of the
first image.  If you want to access images or child properties like `og:image:width` then you
should instead use the `graph.Metadata` dictionary.*
* `graph.Url` is a shortcut for `graph.Metadata["og:url"].Value()`

#### Misc

The original URL used to generate the OpenGraph data is available from the `OriginalUrl` property
`graph.OriginalUrl`.

### Creating OpenGraph Data

To create OpenGraph data in memory use the following code:

```csharp
var graph = OpenGraph.MakeGraph(
    title: "My Title", 
    type: "website", 
    image: "http://example.com/img/img1.png", 
    url: "http://example.com/home", 
    description: "My Description", 
    siteName: "Example.com");
graph.AddMetadata("og", "image", "http://example.com/img/img2.png");
graph.Metadata["og:image"][0].AddProperty("width", "30");
graph.Metadata["og:image"][1].AddProperty("width", "60");
System.Console.Write(graph.ToString());
```

The previous `System.Console.Write(graph.ToString());` will produce the following HTML (formatting added for legibility):

```html
<meta property="og:title" content="My Title">
<meta property="og:type" content="website">
<meta property="og:image" content="http://example.com/img/img1.png">
<meta property="og:image:width" content="30">
<meta property="og:image" content="http://example.com/img/img2.png">
<meta property="og:image:width" content="60">
<meta property="og:url" content="http://example.com/home">
<meta property="og:description" content="My Description">
<meta property="og:site_name" content="Example.com">
```

### Parsing Namespaces

The component now knows about the 13 namespaces listed below.  When parsing a url or a HTML
document, OpenGraph.Net will now read and use those namespaces from either the `<html>` or
`<head>` tags.  The parser is now smart enough to include the namespaces when none are
included in those tags by extracting it from the `meta[property]` value directly.

* og: <http://ogp.me/ns#>
  * Expected fields when validating: `title`, `type`, `image`, `url`
* article: <http://ogp.me/ns/article#>
* book: <http://ogp.me/ns/book#>
* books: <http://ogp.me/ns/books#>
* business <http://ogp.me/ns/business#>
* fitness: <http://ogp.me/ns/fitness#>
* game: <http://ogp.me/ns/game#>
* music: <http://ogp.me/ns/music#>
* place: <http://ogp.me/ns/place#>
* product: <http://ogp.me/ns/product#>
* profile: <http://ogp.me/ns/profile#>
* restaurant: <http://ogp.me/ns/restaurant#>
* video: <http://ogp.me/ns/video#">

*If there are any additional standard/supported namespaces that I am missing, please shoot me
a comment or a pull request with the missing items.*

#### Adding Custom Namespaces

You can now add custom namespaces to the parser.  Simply make the following call:

```csharp
NamespaceRegistry.Instance.AddNamespace(
    prefix: "gah",
    schemaUri: "http://wwww.geoffhorsey.com/ogp/brain#",
    requiredElements: new[] { "brain" });
```

Doing the above will allow the parser to understand the following HTML snippet:

```html
<meta property="gah:brain" content="http://www.geoffhorsey.com/my-brain">
<meta property="gah:brain:size" content="tiny">
```

and the graph:

```csharp
graph.Metadata["gah:brain"].Value() // "http://www.geoffhorsey.com/my-brain"
graph.Metadata["gah:brain"].First().Properties["size"].Value() // "tiny"
```

### Writing out OpenGraph Namespaces

In the wild web sites seem to add their OpenGraph namespaces in one of 2 ways.  They either
write the namespaces in the `html` as `xmlns` attributes or within the `head` tag in the `prefix` attribute.

* `<html xmlns:og="http://ogp.me/ns#" xmlns:product="http://ogp.me/ns/product#">`
* `<head prefix="og: http://ogp.me/ns# product: http://ogp.me/ns/product#">`

**`xmlns:` version in the `html` tag**

To create the `html` version in an cshtml page after creating a new `graph`, use the following code:

```html
<html @graph.HtmlXmlnsValues>
```

Would produce the following:

```html
<html xmlns:og="http://ogp.me/ns#" xmlns:product="http://ogp.me/ns/product#">
```

#### `prefix` version in the `<head>` tag

To create the `head` version in a cshtml page, after create a new `graph`, use the following code:

```html
<head prefix="@graph.HeadPrefixAttributeValue">
```

Would produce the following:

```html
<head prefix="og: http://ogp.me/ns# product: http://ogp.me/ns/product#">
```

### Writing out OpenGraph Metadata to the `head` tag

Below is a complete example to write out a OpenGraph metadata to a page:

```html
@{
    var graph = OpenGraph.MakeGraph(
        title: "My Title", 
        type: "website", 
        image: "http://example.com/img/img1.png", 
        url: "http://example.com/home", 
        description: "My Description", 
        siteName: "Example.com");
}
<html>
<head prefix="@graph.HeadPrefixAttributeValue">
    @graph.ToString()
</head>
<body>
    <!-- Your awesome page! -->
</body>
</html>
```

will produce the following HTML:

```html
<html>
<head prefix="og: http://ogp.me/ns#">
    <meta property="og:title" content="My Title">
    <meta property="og:type" content="website">
    <meta property="og:image" content="http://example.com/img/img1.png">
    <meta property="og:url" content="http://example.com/home">
    <meta property="og:description" content="My Description">
    <meta property="og:site_name" content="Example.com">
</head>
<body>
    <!-- Your awesome page! -->
</body>
</html>
```

## It's FOSS

So please don't be afraid to [fork me](https://github.com/ghorsey/OpenGraph-Net).

### Contribution Guide

1. Fork the OpenGraph-Net repository
2. Create a feature branch for the item you are going to add.
3. Add your awesome code and your unit tests to cover the new feature
4. Run all of the tests to ensure everything is still passing.
5. Create a pull request to our `develop` branch.

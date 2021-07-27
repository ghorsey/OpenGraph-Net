# Parsing Namespaces

The component now knows about the 13 namespaces listed below.  When parsing a url or a HTML
document, OpenGraph.Net will now read and use those namespaces from either the `<html>` or
`<head>` tags.  The parser is now smart enough to include the namespaces when none are
included in those tags by extracting it from the `meta[property]` value directly.

* og: http://ogp.me/ns#
  * Expected fields when validating: `title`, `type`, `image`, `url`
* article: http://ogp.me/ns/article#
* book: http://ogp.me/ns/book#"
* books: http://ogp.me/ns/books#
* business http://ogp.me/ns/business#
* fitness: http://ogp.me/ns/fitness#
* game: http://ogp.me/ns/game#
* music: http://ogp.me/ns/music#
* place: http://ogp.me/ns/place#
* product: http://ogp.me/ns/product#
* profile: http://ogp.me/ns/profile#
* restaurant: http://ogp.me/ns/restaurant#
* video: http://ogp.me/ns/video#"

*If there are any additional standard/supported namespaces that I am missing, please shoot me
a comment or a pull request with the missing items.*

## Adding Custom Namespaces

You can now add custom namespaces to the parser.  Simply make the following call:

```csharp
NamespaceRegistry.Instance.AddNamespace(
    prefix: "gah",
    schemaUri: "http://wwww.geoffhorsey.com/ogp/brain",
    requiredElements: new[] { "brain" });
```

Doing the above will allow the parser to understand the following HTML snippet:

```html
<meta property="gah:brain" content="http://www.geoffhorsey.com/my-brain">
<meta property="gah:brain:size" content="tiny">
```

and the graph:

```csharp
graph.Metadata["gah:brain"].Value(); // "http://www.geoffhorsey.com/my-brain"
graph.Metadata["gah:brain"].First().Properties["size"].Value()// "tiny"
```

# Creating OpenGraph Data

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
```

To write out the meta tags use `graph.ToString();`.  This will produce the following HTML (formatting added for legibility):

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

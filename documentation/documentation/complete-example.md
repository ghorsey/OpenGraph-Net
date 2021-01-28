# Complete Example

Below is a complete example to write out a OpenGraph metadata to a cshtml page:

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

will produce the following HTML:

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

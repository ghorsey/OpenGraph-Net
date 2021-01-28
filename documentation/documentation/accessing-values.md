
# Accessing Values

Learn how the OpenGraph object records values from OpenGraph

## Accessing Metadata

Each metadata element is is stored as an array. Additionally, each element's properties are also stored as an array.

    <meta property="og:image" content="http://example.com/img1.png">
    <meta property="og:image:width" content="30">
    <meta property="og:image" content="http://example.com/img2.png">
    <meta property="og:image:width" content="60">
    <meta property="og:locale" content="en">
    <meta property="og:locale:alternate" content="en_US">
    <meta property="og:locale:alternate" content="en_GB">

You would access the values from the sample HTML above as:

* `graph.Metadata["og:image"].First().Value`  `// "http://example.com/img1.png"`.
* `graph.Metadata["og:image"].First().Properties["width"].Value()` `// "30"`.
* `graph.Metadata["og:image"][1].Value` `// "http://example.com/img2.png"`.
* `graph.Metadata["og:image"][1].Properties["width"].Value()` `// "30"`.
* `graph.Metadata["og:locale"].Value()` `// "en"`
* `graph.Metadata["og:locale"].First().Properties["alternate"][0].Value` `// "en_US"`
* `graph.Metadata["og:locale"].First().Properties["alternate"][1].Value` `// "en_GB"`

## Basic Metadata

The four required Open Graph properties for all pages are available as direct properties on the OpenGraph object.

* `graph.Type` is a shortcut for `graph.Metadata["og:type"].Value()`
* `graph.Title` is a shortcut for `graph.Metadata["og:title"].Value()`
* `graph.Image` is a shortcut for `graph.Metadata["og:image"].Value()` 
*Note: since there can be multiple images, this helper returns the URI of the 
first image.  If you want to access images or child properties like `og:image:width` then you 
should instead use the `graph.Metadata` dictionary.*
* `graph.Url` is a shortcut for `graph.Metadata["og:url"].Value()`

## Original URL

The original url used to generate the OpenGraph data is available from the `OriginalUrl` property
`graph.OriginalUrl`.

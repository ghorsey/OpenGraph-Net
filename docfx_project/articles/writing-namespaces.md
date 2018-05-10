
# Writing OpenGraph Namespaces
In the wild web sites seem to add their OpenGraph namespaces in one of 2 ways.  They either
write the namespaces in the `html` as `xmlns` attributes or withing the `head` tag in the `prefix` attribute.

* `<html xmlns:og="http://ogp.me/ns#" xmlns:product="http://ogp.me/ns/product#">`
* `<head prefix="og: http://ogp.me/ns# product: http://ogp.me/ns/product#">`

## `xmlns:` version in the `html` tag

To create the `html` version in an cshtml page after creating a new `graph`, use the following code:

    <html @graph.HtmlXmlnsValues>

Would produce the following:

    <html xmlns:og="http://ogp.me/ns#" xmlns:product="http://ogp.me/ns/product#">

## `prefix` version in the `<head>` tag

To create the `head` version in a cshtml page, after create a new `graph`, use the following code:

    <head prefix="@graph.HeadPrefixAttributeValue">

Would produce the following:

    <head prefix="og: http://ogp.me/ns# product: http://ogp.me/ns/product#">
 
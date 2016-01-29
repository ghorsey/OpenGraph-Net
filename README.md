OpenGraph-Net
=============
[![AppVeyor](https://img.shields.io/appveyor/ci/GeoffHorsey/opengraph-net.svg)](https://ci.appveyor.com/project/GeoffHorsey/opengraph-net)
[![Nuget](https://img.shields.io/nuget/dt/OpenGraph-Net.svg)](http://www.nuget.org/packages/OpenGraph-Net/)
[![Nuget](https://img.shields.io/nuget/v/OpenGraph-Net.svg)](http://www.nuget.org/packages/OpenGraph-Net/)
[![License](https://img.shields.io/badge/license-MIT-orange.svg)](https://raw.githubusercontent.com/ghorsey/OpenGraph-Net/master/LICENSE)

A simple .net assembly to use to parse Open Graph information from either a URL or an HTML snippet.

Usage
=====
OpenGraph graph = OpenGraph.ParseUrl("http://www.amazon.com/Spaced-Complete-Simon-Pegg/dp/B0019MFY3Q");

You can access each open graph value by passing in arguments to the results.  For example:
graph["Description"] will return the description.

The required Open Graph properties are available view properties on the OpenGraph object as a convenience.

* graph.Type
* graph.Title
* graph.Image
* graph.Url

The original url used to generate the OpenGraph data is available from the OriginalUrl property
graph.OriginalUrl
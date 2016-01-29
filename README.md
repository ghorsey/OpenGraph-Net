OpenGraph-Net
=============
A simple .net assembly to use to parse Open Graph information from either a URL or an HTML snippet.

[![Nuget](https://img.shields.io/nuget/dt/commandlineparser.svg)](http://www.nuget.org/packages/OpenGraph-Net/)
[![Nuget](https://img.shields.io/nuget/v/commandlineparser.svg)](http://www.nuget.org/packages/OpenGraph-Net/)
[![Nuget](https://img.shields.io/badge/license-MIT-blue.svg)

Copyright (c) 2011-2013 Geoff Horsey

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
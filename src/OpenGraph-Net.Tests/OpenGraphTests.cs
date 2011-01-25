using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace OpenGraph_Net.Tests
{
    [TestFixture]
    public class OpenGraphTests
    {
        private string _validSampleContent = @"<!DOCTYPE HTML>
<html>
<head>
    <meta property=""og:type"" content=""product"" />
    <meta property=""og:title"" cOntent=""Product Title"" />
    <meta name=""og:image"" content=""http://www.test.com/test.png""/>
    <meta propErty=""og:uRl"" content=""http://www.test.com"" />
    <meta property=""og:description"" content=""My Description""/>
    <meta property=""og:site_Name"" content=""Test Site"">
</head>
<body>
</body>
</html>";

        private string _invalidSampleContent = @"<!DOCTYPE HTML>
<html>
<head>
    <meta property=""og:title"" cOntent=""Product Title"" />
    <meta name=""og:image"" content=""http://www.test.com/test.png""/>
    <meta propErty=""og:uRl"" content=""http://www.test.com"" />
    <meta property=""og:description"" content=""My Description""/>
    <meta property=""og:site_Name"" content=""Test Site"">
    <meta property=""og:mistake"" value=""not included"">
</head>
<body>
</body>
</html>";

        private string _invalidMissingRequiredUrls = @"<!DOCTYPE HTML>
<html>
<head>
    <meta property=""og:type"" content=""product"" />
    <meta property=""og:title"" cOntent=""Product Title"" />
    <meta property=""og:description"" content=""My Description""/>
    <meta property=""og:site_Name"" content=""Test Site"">
</head>
<body>
</body>
</html>";
        [Test]
        public void TestValidGraphParsing()
        {
            OpenGraph graph = OpenGraph.ParseHtml(_validSampleContent, true);

            Assert.AreEqual("product", graph.Type);
            Assert.AreEqual("Product Title", graph.Title);
            Assert.AreEqual("http://www.test.com/test.png", graph.Image.ToString());
            Assert.AreEqual("http://www.test.com/", graph.Url.ToString());
            Assert.AreEqual("My Description", graph["description"]);
            Assert.AreEqual("Test Site", graph["site_name"]);
        }

        [Test]
        public void TestHtmlMissingUrls()
        {
            OpenGraph graph = OpenGraph.ParseHtml(_invalidMissingRequiredUrls, false);

            Assert.AreEqual("product", graph.Type);
            Assert.AreEqual("Product Title", graph.Title);
            Assert.IsNull(graph.Image);
            Assert.IsNull(graph.Url);
            Assert.AreEqual("My Description", graph["description"]);
            Assert.AreEqual("Test Site", graph["site_name"]);
        }

        [Test, ExpectedException(typeof(InvalidSpecificationException))]
        public void TestInvaidGraphParsing()
        {
            OpenGraph graph = OpenGraph.ParseHtml(_invalidSampleContent, true);
        }

        [Test]
        public void TestInvalidGraphParsingWithoutCheck()
        {
            OpenGraph graph = OpenGraph.ParseHtml(_invalidSampleContent);

            Assert.AreEqual("", graph.Type);
            Assert.IsFalse(graph.ContainsKey("mistake"));
            Assert.AreEqual("Product Title", graph.Title);
            Assert.AreEqual("http://www.test.com/test.png", graph.Image.ToString());
            Assert.AreEqual("http://www.test.com/", graph.Url.ToString());
            Assert.AreEqual("My Description", graph["description"]);
            Assert.AreEqual("Test Site", graph["site_name"]);
        }

        [Test]
        public void TestAmazonUrl()
        {
            OpenGraph graph = OpenGraph.ParseUrl("http://www.amazon.com/Spaced-Complete-Simon-Pegg/dp/B0019MFY3Q");

            Assert.AreEqual("http://www.amazon.com/dp/B0019MFY3Q/ref=tsm_1_fb_lk", graph.Url.ToString());
            Assert.AreEqual("Spaced: The Complete Series", graph.Title);
            Assert.AreEqual("SPACED:COMPLETE SERIES - DVD Movie", graph["description"]);
            Assert.AreEqual("http://ecx.images-amazon.com/images/I/511vJmUeGTL._SL160_.jpg", graph.Image.ToString());
            Assert.AreEqual("movie", graph.Type);
            Assert.AreEqual("Amazon.com", graph["site_name"]);
        }
    }
}

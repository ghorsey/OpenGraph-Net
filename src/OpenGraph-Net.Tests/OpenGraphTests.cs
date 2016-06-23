// <copyright file="OpenGraphTests.cs">
// Geoff Horsey
// </copyright>

namespace OpenGraph_Net.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// The open graph test fixture
    /// </summary>
    [TestFixture]
    public class OpenGraphTests
    {
        /// <summary>
        /// The valid sample content
        /// </summary>
        private string validSampleContent = @"<!DOCTYPE HTML>
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

        /// <summary>
        /// The invalid sample content
        /// </summary>
        private string invalidSampleContent = @"<!DOCTYPE HTML>
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

        /// <summary>
        /// The invalid missing required URLs
        /// </summary>
        private string invalidMissingRequiredUrls = @"<!DOCTYPE HTML>
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

        /// <summary>
        /// The invalid missing all meta
        /// </summary>
        private string invalidMissingAllMeta = @"<!DOCTYPE HTML>
<html>
<head>
    <title>some title</title>
</head>
<body>
</body>
</html>";

        /// <summary>
        /// Tests calling <c>MakeOpenGraph</c> method
        /// </summary>
        public void MakeOpenGraphTest()
        {
            var title = "some title";
            var type = "website";
            var image = "http://www.go.com/someimg.jpg";
            var url = "http://www.go.com/";
            var description = "some description";
            var siteName = "my site";
            var graph = OpenGraph.MakeGraph(title, type, image, url, description, siteName);

            Assert.AreEqual(title, graph.Title);
            Assert.AreEqual(type, graph.Type);
            Assert.AreEqual(image, graph.Image.ToString());
            Assert.AreEqual(url, graph.Url.ToString());
            Assert.AreEqual(description, graph["description"]);
            Assert.AreEqual(siteName, graph["site_name"]);

            var expected = "<meta property=\"og:title\" content=\"some title\">" +
                "<meta property=\"og:type\" content=\"website\">" +
                "<meta property=\"og:image\" content=\"http://www.go.com/someimg.jpg\">" +
                "<meta property=\"og:url\" content=\"http://www.go.com/\">" +
                "<meta property=\"og:description\" content=\"some description\">" +
                "<meta property=\"og:site_name\" content=\"my site\">";
            Assert.AreEqual(expected, graph.ToString());
        }

        /// <summary>
        /// Tests parsing the HTML
        /// </summary>
        [Test]
        public void ParseHtmlValidGraphParsingTest()
        {
            OpenGraph graph = OpenGraph.ParseHtml(this.validSampleContent, true);

            Assert.AreEqual("product", graph.Type);
            Assert.AreEqual("Product Title", graph.Title);
            Assert.AreEqual("http://www.test.com/test.png", graph.Image.ToString());
            Assert.AreEqual("http://www.test.com/", graph.Url.ToString());
            Assert.AreEqual("My Description", graph["description"]);
            Assert.AreEqual("Test Site", graph["site_name"]);
        }

        /// <summary>
        /// Tests parsing the HTML that is missing URLs
        /// </summary>
        [Test]
        public void ParseHtmlHtmlMissingUrlsTest()
        {
            OpenGraph graph = OpenGraph.ParseHtml(this.invalidMissingRequiredUrls);

            Assert.AreEqual("product", graph.Type);
            Assert.AreEqual("Product Title", graph.Title);
            Assert.IsNull(graph.Image);
            Assert.IsNull(graph.Url);
            Assert.AreEqual("My Description", graph["description"]);
            Assert.AreEqual("Test Site", graph["site_name"]);
        }

        /// <summary>
        /// Test that parsing the HTML with invalid graph specification throws an exception
        /// </summary>
        [Test]
        public void ParseHtmlInvalidGraphParsingTest()
        {
            Assert.Throws<InvalidSpecificationException>(() => OpenGraph.ParseHtml(this.invalidSampleContent, true));
        }

        /// <summary>
        /// Test that parsing the HTML with invalid graph specification throws an exception
        /// </summary>
        [Test]
        public void ParseHtmlInvalidGraphParsingMissingAllMetaTest()
        {
            Assert.Throws<InvalidSpecificationException>(() => OpenGraph.ParseHtml(this.invalidMissingAllMeta, true));
        }

        /// <summary>
        /// Test that parsing the HTML with invalid graph specification passes when validate specification boolean is off
        /// </summary>
        [Test]
        public void ParseHtmlInvalidGraphParsingWithoutCheckTest()
        {
            OpenGraph graph = OpenGraph.ParseHtml(this.invalidSampleContent);

            Assert.AreEqual(string.Empty, graph.Type);
            Assert.IsFalse(graph.ContainsKey("mistake"));
            Assert.AreEqual("Product Title", graph.Title);
            Assert.AreEqual("http://www.test.com/test.png", graph.Image.ToString());
            Assert.AreEqual("http://www.test.com/", graph.Url.ToString());
            Assert.AreEqual("My Description", graph["description"]);
            Assert.AreEqual("Test Site", graph["site_name"]);
        }

        /// <summary>
        /// Test parsing a URL
        /// </summary>
        [Test]
        public void ParseUrlAmazonUrlTest()
        {
            OpenGraph graph = OpenGraph.ParseUrl("http://www.amazon.com/Spaced-Complete-Simon-Pegg/dp/B0019MFY3Q");

            Assert.AreEqual("http://www.amazon.com/dp/B0019MFY3Q/ref=tsm_1_fb_lk", graph.Url.ToString());
            Assert.IsTrue(graph.Title.StartsWith("Spaced: The Complete Series"));
            Assert.IsTrue(graph["description"].Contains("Spaced"));
            Assert.IsTrue(graph.Image.ToString().Contains("images-amazon"));
            Assert.AreEqual("movie", graph.Type);
            Assert.AreEqual("Amazon.com", graph["site_name"]);
        }

        [Test]
        public void ParseUrlValidateEncodingIsCorrect()
        {
            var expectedContent =
                "Создайте себе горное настроение с нашим первым фан-китом по игре #SteepGame&amp;#33; -&amp;gt; http://ubi.li/u8w9n";
            var tags = OpenGraph.ParseUrl("https://vk.com/wall-41600377_66756");

            Assert.That(tags["description"], Is.EqualTo(expectedContent));
        }
    }
}

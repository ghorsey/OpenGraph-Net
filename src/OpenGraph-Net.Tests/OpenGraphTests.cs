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

        private string _invalidMissingAllMeta = @"<!DOCTYPE HTML>
<html>
<head>
    <title>some title</title>
</head>
<body>
</body>
</html>";

        public void MakeOpenGraph_Test()
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
        [Test]
        public void ParseHtml_ValidGraphParsing_Test()
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
        public void ParseHtml_HtmlMissingUrls_Test()
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
        public void ParseHtml_InvaidGraphParsing_Test()
        {
            OpenGraph graph = OpenGraph.ParseHtml(_invalidSampleContent, true);
        }

        [Test, ExpectedException(typeof(InvalidSpecificationException))]
        public void ParseHtml_InvaidGraphParsingMissingAllMeta_Test()
        {
            OpenGraph graph = OpenGraph.ParseHtml(_invalidMissingAllMeta, true);
        }

        [Test]
        public void ParseHtml_InvalidGraphParsingWithoutCheck_Test()
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
        public void ParseUrl_AmazonUrl_Test()
        {
            OpenGraph graph = OpenGraph.ParseUrl("http://www.amazon.com/Spaced-Complete-Simon-Pegg/dp/B0019MFY3Q");

            Assert.AreEqual("http://www.amazon.com/dp/B0019MFY3Q/ref=tsm_1_fb_lk", graph.Url.ToString());
            Assert.AreEqual("Spaced: The Complete Series", graph.Title);
            Assert.IsTrue(graph["description"].Contains("Spaced"));
            Assert.AreEqual("http://ecx.images-amazon.com/images/I/51sWMo4CBcL._SL160_.jpg", graph.Image.ToString());
            Assert.AreEqual("movie", graph.Type);
            Assert.AreEqual("Amazon.com", graph["site_name"]);
        }
    }
}

﻿// <copyright file="OpenGraphTests.cs">
// Geoff Horsey
// </copyright>

namespace OpenGraph_Net.Tests
{
    using System.Threading.Tasks;
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
        public void TestMakingOpenGraphMetaTags()
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
        public void TestParsingHtmlValidGraphParsingTest()
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
        public void TestParsingHtmlHtmlMissingUrlsTest()
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
        public void TestParsingHtmlInvalidGraphParsingTest()
        {
            Assert.Throws<InvalidSpecificationException>(() => OpenGraph.ParseHtml(this.invalidSampleContent, true));
        }

        /// <summary>
        /// Test that parsing the HTML with invalid graph specification throws an exception
        /// </summary>
        [Test]
        public void TestParsingHtmlInvalidGraphParsingMissingAllMetaTest()
        {
            Assert.Throws<InvalidSpecificationException>(() => OpenGraph.ParseHtml(this.invalidMissingAllMeta, true));
        }

        /// <summary>
        /// Test that parsing the HTML with invalid graph specification passes when validate specification boolean is off
        /// </summary>
        [Test]
        public void TestParsingHtmlInvalidGraphParsingWithoutCheckTest()
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
        /// Tests the parsing amazon URL asynchronous test.
        /// </summary>
        [Test]
        public async Task TestParsingAmazonUrlAsyncTest()
        {
            OpenGraph graph = await OpenGraph.ParseUrlAsync("http://www.amazon.com/Spaced-Complete-Simon-Pegg/dp/B0019MFY3Q");

            Assert.AreEqual("http://www.amazon.com/dp/B0019MFY3Q/ref=tsm_1_fb_lk", graph.Url.ToString());
            Assert.IsTrue(graph.Title.StartsWith("Spaced: The Complete Series"));
            Assert.IsTrue(graph["description"].Contains("Spaced"));
            Assert.IsTrue(graph.Image.ToString().Contains("images-amazon"));
            Assert.AreEqual("movie", graph.Type);
            Assert.AreEqual("Amazon.com", graph["site_name"]);
        }

        /// <summary>
        /// Tests the parsing URL asynchronous validate encoding is correct.
        /// </summary>
        [Test]
        public async Task TestParsingUrlAsyncValidateEncodingIsCorrect()
        {
            var expectedContent =
                "Создайте себе горное настроение с нашим первым фан-китом по игре #SteepGame&amp;#33; -&amp;gt; http://ubi.li/u8w9n";
            var tags = await OpenGraph.ParseUrlAsync("https://vk.com/wall-41600377_66756");

            Assert.That(tags["description"], Is.EqualTo(expectedContent));
        }
        /// <summary>
        /// Test parsing a URL
        /// </summary>
        [Test]
        public void TestParsingAmazonUrlTest()
        {
            OpenGraph graph = OpenGraph.ParseUrl("http://www.amazon.com/Spaced-Complete-Simon-Pegg/dp/B0019MFY3Q");

            Assert.AreEqual("http://www.amazon.com/dp/B0019MFY3Q/ref=tsm_1_fb_lk", graph.Url.ToString());
            Assert.IsTrue(graph.Title.StartsWith("Spaced: The Complete Series"));
            Assert.IsTrue(graph["description"].Contains("Spaced"));
            Assert.IsTrue(graph.Image.ToString().Contains("images-amazon"));
            Assert.AreEqual("movie", graph.Type);
            Assert.AreEqual("Amazon.com", graph["site_name"]);
        }

        /// <summary>
        /// Tests the parsing URL validate encoding is correct.
        /// </summary>
        [Test]
        public void TestParsingUrlValidateEncodingIsCorrect()
        {
            var expectedContent =
                "Создайте себе горное настроение с нашим первым фан-китом по игре #SteepGame&amp;#33; -&amp;gt; http://ubi.li/u8w9n";
            var tags = OpenGraph.ParseUrl("https://vk.com/wall-41600377_66756");

            Assert.That(tags["description"], Is.EqualTo(expectedContent));
        }

        /// <summary>
        /// Tests the meta charset parses correctly.
        /// </summary>
        [Test]
        public void TestMetaCharsetParsesCorrectly()
        {
            var expectedTitle = "Réalité virtuelle : 360° de bonheur à améliorer - Cinéma - Télérama.fr";
            var expectedDescription =
                "Le cinéma à 360° a désormais son festival. Organisé par le Forum des images, le premier Paris Virtual Film Festival a donc vu le jour. Narration, réalisation, montage… une révolution balbutiante est en marche. Tour d&#039;horizon.";

            var ogs = OpenGraph.ParseUrl("http://www.telerama.fr/cinema/realite-virtuelle-360-de-bonheur-a-ameliorer,144339.php?utm_medium=Social&utm_source=Twitter&utm_campaign=Echobox&utm_term=Autofeed#link_time=1466595239");

            Assert.AreEqual(expectedTitle, ogs["title"]);
            Assert.AreEqual(expectedDescription, ogs["description"]);
        }


        [Test]
        public void TestUrlDecodingUrlValues()
        {
            var expectedUrl =
                "https://tn.periscope.tv/lXc5gSh6UPaWdc37LtVCb3UdtSfvj2QNutojPK2du5YWrNchfI4wXpwwHKTyfDhmfT2ibsBZV4doQeWlhSvI4A==/chunk_314.jpg?Expires=1781852253&Signature=U5OY3Y2HRb4ETmakQAPwMcv~bqu6KygIxriooa41rk64RcDfjww~qpVgMR-T1iX4S9NxfvXHLMT3pEckBDEOicsNO7oUAo4NieH9GRB2Sv0EA7swxLojD~Zn98ThNWTF5fSzv6SSPjyvctsqBiRmvAN6x7fmMH6l3vzx8ePSCgdEm8-31lUAz7lReBNZQjYSi~C8AwqZVI0Mx6y8lNKklL~m0e6RTGdvr~-KIDewU3wpjSdX7AgpaXXjahk4x-ceUUKcH3T1j--ZjaY7nqPO9fbMZFNPs502A32mrcmaZCzvaD~AuoH~u3y44mJVjzHRrpTxHIBklqHxAgc7dzverg__&Key-Pair-Id=APKAIHCXHHQVRTVSFRWQ";
            var og = OpenGraph.ParseUrl("https://www.periscope.tv/w/1DXxyZZZVykKM");


            Assert.AreEqual(expectedUrl, og["image"]);
        }
    }
}

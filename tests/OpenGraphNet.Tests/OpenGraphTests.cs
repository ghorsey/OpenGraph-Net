#pragma warning disable 618
namespace OpenGraphNet.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using OpenGraphNet;
    using OpenGraphNet.Metadata;
    using OpenGraphNet.Namespaces;

    using Xunit;

    /// <summary>
    /// The open graph test fixture.
    /// </summary>
    public class OpenGraphTests
    {
        /// <summary>
        /// The spaced link.
        /// </summary>
        private const string SpacedLink = "http://www.imdb.com/title/tt0187664/";

        /// <summary>
        /// The periscope TV content.
        /// </summary>
        private const string PeriscopeTvContent = @"<!doctype html>
<html>
<head>
    <title data-react-helmet=""true"">Société Générale: &quot;Accueil musical pour les collaborateurs en cette #FDLM2016 #TeamSpirit&quot;</title>
    <meta data-react-helmet=""true"" property=""og:title"" content=""Société Générale @SocieteGenerale"" />
    <meta data-react-helmet=""true"" property=""og:description"" content=""Accueil musical pour les collaborateurs en cette #FDLM2016 #TeamSpirit — Nanterre, France"" />
    <meta data-react-helmet=""true"" property=""og:image"" content=""https://prod-video-us-west-2.pscp.tv/lXc5gSh6UPaWdc37LtVCb3UdtSfvj2QNutojPK2du5YWrNchfI4wXpwwHKTyfDhmfT2ibsBZV4doQeWlhSvI4A==/replay_thumbnail/us-west-2/periscope-replay-direct-live/eyJkIjowfQ/chunk_314.jpg?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsInZlcnNpb24iOiIyIn0.eyJBbGxvd2VkUHJvdG9jb2xzIjpbInRodW1iIl0sIkJyb2FkY2FzdElkIjoiMURYeHlaWlpWeWtLTSIsIkdyYW50VHlwZSI6InJlYWQiLCJHcmFudGVkQXQiOjE1NDQwNTgzMjgsIkdyYW50ZWRUbyI6IndlYi1yYXRlLWxpbWl0ZWQtMDA4NjkxZjE3ZDUyY2E3OTMyNTdlYWQ4NTkyOGE1N2IiLCJTdHJlYW1OYW1lIjoibFhjNWdTaDZVUGFXZGMzN0x0VkNiM1VkdFNmdmoyUU51dG9qUEsyZHU1WVdyTmNoZkk0d1hwd3dIS1R5ZkRobWZUMmlic0JaVjRkb1FlV2xoU3ZJNEE9PSIsImV4cCI6MTU0NDIzMTEyOH0.UbJfSn8A0BFBRJHAlhgBxAxGv4DuMnTnwGOztR-3swE&amp;service=proxsee&amp;digest=tGkjjGWVywQpODOMKx7vAqtCh-QohM9rSCR8KM_JtN4"" />
    <meta data-react-helmet=""true"" property=""og:site_name"" content=""Periscope"" />
    <meta data-react-helmet=""true"" property=""og:type"" content=""video"" />
    <meta data-react-helmet=""true"" property=""og:url"" content=""https://www.pscp.tv/SocieteGenerale/1DXxyZZZVykKM"" />
    <meta data-react-helmet=""true"" property=""og:email"" content=""contact@pscp.tv"" />
    <meta data-react-helmet=""true"" property=""og:image:width"" content=""320"" />
    <meta data-react-helmet=""true"" property=""og:image:height"" content=""568"" />
</head>
<body class=""""></body></html>";

        /// <summary>
        /// The Spotify album content.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private const string SpotifyAlbumContent = @"<!DOCTYPE html>
<html lang=en class="""">
<head>
    <meta charset=""UTF-8"">
    <meta property=""og:title"" content=""Salutations"">
    <meta property=""og:description"" content="""">
    <meta property=""og:url"" content=""https://open.spotify.com/album/5YQGQfkjghbxW00eKy9YpJ"">
    <meta property=""og:image"" content="""">
    <meta property=""og:type"" content=""music.album"">
    <meta property=""music:musician"" content=""https://open.spotify.com/artist/2Z7gV3uEh1ckIaBzTUCE6R"">
    <meta property=""music:release_date"" content=""2017-03-17"">
    <meta property=""music:song"" content=""https://open.spotify.com/track/1JJUbiYekbYkdDhK1kp3C9"">
    <meta property=""music:song:disc"" content=""1"">
    <meta property=""music:song:track"" content=""1"">
    <meta property=""music:song"" content=""https://open.spotify.com/track/3eitV6XbyRW0FxKEUh60Pi"">
    <meta property=""music:song:disc"" content=""1"">
    <meta property=""music:song:track"" content=""2"">
    <title>Spotify Web Player</title>
    <link rel=""icon"" href=""https://open.scdn.co/static/images/favicon.png "">
    <link rel=""stylesheet"" href=""https://open.scdn.co/static/web-player.97c0abb0.css"" />
    <link rel=""manifest"" href=""/static/manifest.json"">
    <link rel=""preconnect"" href=""https://api.spotify.com"">
    <link rel=""preconnect"" href=""https://spclient.wg.spotify.com"">
    <link rel=""preconnect"" href=""https://apresolve.spotify.com"">
    <meta property=""og:locale"" content=""es"">
    <meta property=""og:locale:alternate"" content=""es_US"">
    <meta property=""og:locale:alternate"" content=""es_ES"">
</head>
<body class=""env-prod "" data-locale=""en"" data-market=""US""></body></html>";

        /// <summary>
        /// The spotify playlist content.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private const string SpotifyPlaylistContent = @"<!DOCTYPE html>
<html lang=en class="""">
<head>
    <meta charset=""UTF-8"">
    <meta property=""og:title"" content=""Programming Jams, a playlist by Jefe on Spotify"">
    <meta property=""og:description"" content="""">
    <meta property=""og:url"" content=""https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe/playlist/2lzTTRqhYS6AkHPIvdX9u3"">
    <meta property=""og:image"" content="""">
    <meta property=""og:type"" content=""music.playlist"">
    <meta property=""music:creator"" content=""https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe"">
    <meta property=""music:song_count"" content=""1020"">
    <meta property=""music:song"" content=""https://open.spotify.com/track/3RL1cNdki1AsOLCMinb60a"">
    <meta property=""music:song:track"" content=""1"">
    <meta property=""music:song"" content=""https://open.spotify.com/track/4yVfG04odefa7JanoF5r86"">
    <meta property=""music:song:track"" content=""2"">
    <meta property=""og:restrictions:country:allowed"" content=""AD"">
    <meta property=""og:restrictions:country:allowed"" content=""AR"">
    <title>Spotify Web Player</title>
    <link rel=""icon"" href=""https://open.scdn.co/static/images/favicon.png "">
    <link rel=""stylesheet"" href=""https://open.scdn.co/static/web-player.97c0abb0.css"" />
    <link rel=""manifest"" href=""/static/manifest.json"">
    <link rel=""preconnect"" href=""https://api.spotify.com"">
    <link rel=""preconnect"" href=""https://spclient.wg.spotify.com"">
    <link rel=""preconnect"" href=""https://apresolve.spotify.com"">
    <meta property=""og:locale:alternate"" content=""en_US"">
    <meta property=""og:locale:alternate"" content=""en_GB"">
    </head>
<body class=""env-prod "" data-locale=""en"" data-market=""US""></body>
</html>";

        /// <summary>
        /// The valid sample content.
        /// </summary>
        private readonly string validSampleContent = @"<!DOCTYPE HTML>
<html>
<head prefix=""og: http://ogp.me/ns# product: http://ogp.me/ns/product#"">
    <meta property=""og:type"" content=""product"" />
    <meta property=""og:title"" cOntent=""Product Title"" />
    <meta name=""og:image"" content=""http://www.test.com/test.png""/>
    <meta propErty=""og:uRl"" content=""http://www.test.com"" />
    <meta property=""og:description"" content=""My Description""/>
    <meta property=""og:site_Name"" content=""Test Site"">
    <meta property=""gah:pea_brain:size"" content=""small"">
</head>
<body>
</body>
</html>";

        /// <summary>
        /// The invalid sample content.
        /// </summary>
        private readonly string invalidSampleContent = @"<!DOCTYPE HTML>
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
        /// The invalid missing required URLs.
        /// </summary>
        private readonly string invalidMissingRequiredUrls = @"<!DOCTYPE HTML>
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
        /// The invalid missing all meta.
        /// </summary>
        private readonly string invalidMissingAllMeta = @"<!DOCTYPE HTML>
<html>
<head>
    <title>some title</title>
</head>
<body>
</body>
</html>";

        /// <summary>
        /// Tests the parsing spotify album.
        /// </summary>
        [Fact]
        public void TestParsingSpotifyAlbum()
        {
            var graph = OpenGraph.ParseHtml(SpotifyAlbumContent);

            Assert.Equal("Salutations", graph.Title);
            Assert.Empty(graph.Metadata["og:description"].First().Value);
            Assert.Equal("https://open.spotify.com/album/5YQGQfkjghbxW00eKy9YpJ", graph.Url.ToString());
            Assert.Null(graph.Image);
            Assert.Equal("music.album", graph.Type);
            Assert.Equal("https://open.spotify.com/artist/2Z7gV3uEh1ckIaBzTUCE6R", graph.Metadata["music:musician"].First().Value);
            Assert.Equal("2017-03-17", graph.Metadata["music:release_date"].Value());
            Assert.Equal(2, graph.Metadata["music:song"].Count);
            Assert.Equal("https://open.spotify.com/track/1JJUbiYekbYkdDhK1kp3C9", graph.Metadata["music:song"][0].Value);
            Assert.Equal("1", graph.Metadata["music:song"][0].Properties["disc"].Value());
            Assert.Equal("1", graph.Metadata["music:song"][0].Properties["track"].Value());
            Assert.Equal("https://open.spotify.com/track/3eitV6XbyRW0FxKEUh60Pi", graph.Metadata["music:song"][1].Value);
            Assert.Equal("1", graph.Metadata["music:song"][1].Properties["disc"].Value());
            Assert.Equal("2", graph.Metadata["music:song"][1].Properties["track"].Value());
            Assert.Equal("es", graph.Metadata["og:locale"].Value());
            Assert.Equal("es_US", graph.Metadata["og:locale"].First().Properties["alternate"][0].Value);
            Assert.Equal("es_ES", graph.Metadata["og:locale"].First().Properties["alternate"][1].Value);

            Assert.Equal(@"<meta property=""og:title"" content=""Salutations""><meta property=""og:description"" content=""""><meta property=""og:url"" content=""https://open.spotify.com/album/5YQGQfkjghbxW00eKy9YpJ""><meta property=""og:image"" content=""""><meta property=""og:type"" content=""music.album""><meta property=""music:musician"" content=""https://open.spotify.com/artist/2Z7gV3uEh1ckIaBzTUCE6R""><meta property=""music:release_date"" content=""2017-03-17""><meta property=""music:song"" content=""https://open.spotify.com/track/1JJUbiYekbYkdDhK1kp3C9""><meta property=""music:song:disc"" content=""1""><meta property=""music:song:track"" content=""1""><meta property=""music:song"" content=""https://open.spotify.com/track/3eitV6XbyRW0FxKEUh60Pi""><meta property=""music:song:disc"" content=""1""><meta property=""music:song:track"" content=""2""><meta property=""og:locale"" content=""es""><meta property=""og:locale:alternate"" content=""es_US""><meta property=""og:locale:alternate"" content=""es_ES"">", graph.ToString());
            Assert.Equal("og: http://ogp.me/ns# music: http://ogp.me/ns/music#", graph.HeadPrefixAttributeValue);
            Assert.Equal("xmlns:og=\"http://ogp.me/ns#\" xmlns:music=\"http://ogp.me/ns/music#\"", graph.HtmlXmlnsValues);
        }

        /// <summary>
        /// Tests the parsing spotify playlist.
        /// </summary>
        [Fact]
        public void TestParsingSpotifyPlaylist()
        {
            var graph = OpenGraph.ParseHtml(SpotifyPlaylistContent);

            Assert.Equal("Programming Jams, a playlist by Jefe on Spotify", graph.Title);
            Assert.Equal(string.Empty, graph.Metadata["og:description"].First().Value);
            Assert.Equal("https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe/playlist/2lzTTRqhYS6AkHPIvdX9u3", graph.Url.ToString());
            Assert.Null(graph.Image);
            Assert.Equal("music.playlist", graph.Type);
            Assert.Equal("https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe", graph.Metadata["music:creator"].First().Value);
            Assert.Equal("1020", graph.Metadata["music:song_count"].First().Value);
            Assert.Equal(2, graph.Metadata["music:song"].Count);
            Assert.Equal("https://open.spotify.com/track/3RL1cNdki1AsOLCMinb60a", graph.Metadata["music:song"][0].Value);
            Assert.Equal("1", graph.Metadata["music:song"][0].Properties["track"].First().Value);
            Assert.Equal("https://open.spotify.com/track/4yVfG04odefa7JanoF5r86", graph.Metadata["music:song"][1].Value);
            Assert.Equal("2", graph.Metadata["music:song"][1].Properties["track"].First().Value);
            Assert.Equal("AD", graph.Metadata["og:restrictions:country:allowed"][0].Value);
            Assert.Equal("AR", graph.Metadata["og:restrictions:country:allowed"][1].Value);
            Assert.Equal("en_US", graph.Metadata["og:locale:alternate"][0].Value);
            Assert.Equal("en_GB", graph.Metadata["og:locale:alternate"][1].Value);

            Assert.Equal(@"<meta property=""og:title"" content=""Programming Jams, a playlist by Jefe on Spotify""><meta property=""og:description"" content=""""><meta property=""og:url"" content=""https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe/playlist/2lzTTRqhYS6AkHPIvdX9u3""><meta property=""og:image"" content=""""><meta property=""og:type"" content=""music.playlist""><meta property=""music:creator"" content=""https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe""><meta property=""music:song_count"" content=""1020""><meta property=""music:song"" content=""https://open.spotify.com/track/3RL1cNdki1AsOLCMinb60a""><meta property=""music:song:track"" content=""1""><meta property=""music:song"" content=""https://open.spotify.com/track/4yVfG04odefa7JanoF5r86""><meta property=""music:song:track"" content=""2""><meta property=""og:restrictions:country:allowed"" content=""AD""><meta property=""og:restrictions:country:allowed"" content=""AR""><meta property=""og:locale:alternate"" content=""en_US""><meta property=""og:locale:alternate"" content=""en_GB"">", graph.ToString());
            Assert.Equal("og: http://ogp.me/ns# music: http://ogp.me/ns/music#", graph.HeadPrefixAttributeValue);
            Assert.Equal("xmlns:og=\"http://ogp.me/ns#\" xmlns:music=\"http://ogp.me/ns/music#\"", graph.HtmlXmlnsValues);
        }

        /// <summary>
        /// Defines the test method TestParsingYouTubeTagsInBody.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestParsingYouTubeTagsInBody()
        {
            var html = await File.ReadAllTextAsync("./TestHtml/YoutubeTagsInBody.htm");

            var graph = OpenGraph.ParseHtml(html);

            Assert.NotEmpty(graph.Metadata);
            Assert.Equal("YouTube", graph.Metadata["og:site_name"].First());
            Assert.Equal("https://www.youtube.com/watch?v=c_-ViUmhqtk", graph.Url.ToString());
            Assert.Equal("PVE Corvette Build Guide | Cucumber Update!", graph.Title);
            Assert.Equal("https://i.ytimg.com/vi/c_-ViUmhqtk/maxresdefault.jpg", graph.Image.ToString());
            Assert.Equal("1280", graph.Metadata["og:image"].First().Properties["width"].First());
            Assert.Equal("720", graph.Metadata["og:image"].First().Properties["height"].First());
            Assert.Equal("The merch store is open!! Take a look▶️ https://teespring.com/stores/down-to-earth-storeWant to help support the channel? Get you name listed at the end of m...", graph.Metadata["og:description"].First());
            Assert.Equal("The merch store is open!! Take a look▶️ https://teespring.com/stores/down-to-earth-storeWant to help support the channel? Get you name listed at the end of m...", graph.Metadata["og:description"].First());
            Assert.Equal("video.other", graph.Type);
            Assert.Equal("https://www.youtube.com/embed/c_-ViUmhqtk", graph.Metadata["og:video:url"].First());
            Assert.Equal("https://www.youtube.com/embed/c_-ViUmhqtk", graph.Metadata["og:video:secure_url"].First());
            Assert.Equal("text/html", graph.Metadata["og:video:type"].First());
            Assert.Equal("1280", graph.Metadata["og:video:width"].First());
            Assert.Equal("720", graph.Metadata["og:video:height"].First());
            Assert.Equal("Elite dangerous", graph.Metadata["og:video:tag"][0]);
            Assert.Equal("corvette", graph.Metadata["og:video:tag"][1]);
            Assert.Equal("PVE", graph.Metadata["og:video:tag"][2]);
            Assert.Equal("build", graph.Metadata["og:video:tag"][3]);
            Assert.Equal("loadout", graph.Metadata["og:video:tag"][4]);
            Assert.Equal("fitting", graph.Metadata["og:video:tag"][5]);
            Assert.Equal("guide", graph.Metadata["og:video:tag"][6]);
            Assert.Equal("tutorial", graph.Metadata["og:video:tag"][7]);
            Assert.Equal("help", graph.Metadata["og:video:tag"][8]);
            Assert.Equal("2020", graph.Metadata["og:video:tag"][9]);
            Assert.Equal("D2EA", graph.Metadata["og:video:tag"][10]);
            Assert.Equal("shield", graph.Metadata["og:video:tag"][11]);
            Assert.Equal("cucumber", graph.Metadata["og:video:tag"][12]);
            Assert.Equal("newest", graph.Metadata["og:video:tag"][13]);
        }

        /// <summary>
        /// Tests calling <c>MakeOpenGraph</c> method.
        /// </summary>
        [Fact]
        public void TestMakingOpenGraphMetaTags()
        {
            var title = "some title";
            var type = "website";
            var image = "http://www.go.com/img1.png";
            var image2 = "http://www.go.com/img2.png";
            var url = "http://www.go.com/";
            var description = "some description";
            var siteName = "my site";
            var width1 = "30";
            var width2 = "60";
            var graph = OpenGraph.MakeGraph(title, type, image, url, description, siteName);
            graph.AddMetadata("og", "image", image2);
            var imageMetadata = graph.Metadata["og:image"];
            imageMetadata[0].AddProperty("width", width1);
            imageMetadata[1].AddProperty("width", width2);

            Assert.Equal("og", graph.Namespaces.First().Value.Prefix);
            Assert.Equal("http://ogp.me/ns#", graph.Namespaces.First().Value.SchemaUri.ToString());

            Assert.Equal(graph.Namespaces.First().Value, graph.Metadata["og:title"].Namespace());
            Assert.Equal(graph.Metadata["og:image"][0].Name, graph.Metadata["og:image"].Name());
            Assert.Equal(graph.Namespaces.First().Value, graph.Metadata["og:image"].First().Properties["width"].Namespace());
            Assert.Equal(graph.Metadata["og:image"].First().Properties["width"].First().Name, graph.Metadata["og:image"].First().Properties["width"].Name());
            Assert.Equal(title, graph.Title);
            Assert.Equal(type, graph.Type);
            Assert.Equal(image, graph.Image.ToString());
            Assert.Equal(width1, graph.Metadata["og:image"].First().Properties["width"].Value());
            Assert.Equal(image2, graph.Metadata["og:image"][1].Value);
            Assert.Equal(width2, graph.Metadata["og:image"][1].Properties["width"].Value());
            Assert.Equal(url, graph.Url.ToString());
            Assert.Equal(description, graph.Metadata["og:description"].Value());
            Assert.Equal(description, graph.Metadata["description"].Value());
            Assert.Equal(siteName, graph.Metadata["og:site_name"].Value());
            Assert.Equal(siteName, graph.Metadata["site_name"].Value());
            Assert.Equal(0, graph.Metadata["og:donotexist"].Count);

            var expected = $"<meta property=\"og:title\" content=\"{title}\">" +
                "<meta property=\"og:type\" content=\"website\">" +
                $"<meta property=\"og:image\" content=\"{image}\">" +
                $"<meta property=\"og:image:width\" content=\"{width1}\">" +
                $"<meta property=\"og:image\" content=\"{image2}\">" +
                $"<meta property=\"og:image:width\" content=\"{width2}\">" +
                $"<meta property=\"og:url\" content=\"{url}\">" +
                $"<meta property=\"og:description\" content=\"{description}\">" +
                $"<meta property=\"og:site_name\" content=\"{siteName}\">";
            Assert.Equal(expected, graph.ToString());
        }

        /// <summary>
        /// Tests parsing the HTML.
        /// </summary>
        [Fact]
        public void TestParsingHtmlValidGraphParsingTest()
        {
            NamespaceRegistry.Instance.AddNamespace("gah", "http://www.geoffhorsey.com/ogp/pea_brain#", "pea_brain:size");
            OpenGraph graph = OpenGraph.ParseHtml(this.validSampleContent, true);

            Assert.Equal(3, graph.Namespaces.Count);
            Assert.Equal("og", graph.Namespaces["og"].Prefix);
            Assert.Equal("http://ogp.me/ns#", graph.Namespaces["og"].SchemaUri.ToString());
            Assert.Equal("product", graph.Namespaces["product"].Prefix);
            Assert.Equal("http://ogp.me/ns/product#", graph.Namespaces["product"].SchemaUri.ToString());
            Assert.Equal("gah", graph.Namespaces["gah"].Prefix);
            Assert.Equal("http://www.geoffhorsey.com/ogp/pea_brain#", graph.Namespaces["gah"].SchemaUri.ToString());

            Assert.Equal("product", graph.Type);
            Assert.Equal("Product Title", graph.Title);
            Assert.Equal("http://www.test.com/test.png", graph.Image.ToString());
            Assert.Equal("http://www.test.com/", graph.Url.ToString());
            Assert.Equal("My Description", graph.Metadata["og:description"].First().Value);
            Assert.Equal("My Description", graph.Metadata["description"].Value());
            Assert.Equal("Test Site", graph.Metadata["og:site_name"].First().Value);
            Assert.Equal("Test Site", graph.Metadata["og:site_name"].Value());
            Assert.Equal("small", graph.Metadata["gah:pea_brain:size"].First().Value);
            NamespaceRegistry.Instance.RemoveNamespace("gah");
        }

        /// <summary>
        /// Tests parsing the HTML that is missing URLs.
        /// </summary>
        [Fact]
        public void TestParsingHtmlHtmlMissingUrlsTest()
        {
            OpenGraph graph = OpenGraph.ParseHtml(this.invalidMissingRequiredUrls);

            Assert.Equal(1, graph.Namespaces.Count);
            Assert.Equal("og", graph.Namespaces["og"].Prefix);
            Assert.Equal("http://ogp.me/ns#", graph.Namespaces["og"].SchemaUri.ToString());
            Assert.Equal("product", graph.Type);
            Assert.Equal("Product Title", graph.Title);
            Assert.Null(graph.Image);
            Assert.Null(graph.Url);
            Assert.Equal("My Description", graph.Metadata["og:description"].First().Value);
            Assert.Equal("My Description", graph.Metadata["description"].Value());
            Assert.Equal("Test Site", graph.Metadata["og:site_name"].First().Value);
            Assert.Equal("Test Site", graph.Metadata["site_name"].Value());
        }

        /// <summary>
        /// Test that parsing the HTML with invalid graph specification throws an exception.
        /// </summary>
        [Fact]
        public void TestParsingHtmlInvalidGraphParsingTest()
        {
            Assert.Throws<InvalidSpecificationException>(() => OpenGraph.ParseHtml(this.invalidSampleContent, true));
        }

        /// <summary>
        /// Test that parsing the HTML with invalid graph specification throws an exception.
        /// </summary>
        [Fact]
        public void TestParsingHtmlInvalidGraphParsingMissingAllMetaTest()
        {
            Assert.Throws<InvalidSpecificationException>(() => OpenGraph.ParseHtml(this.invalidMissingAllMeta, true));
        }

        /// <summary>
        /// Test that parsing the HTML with invalid graph specification passes when validate specification boolean is off.
        /// </summary>
        [Fact]
        public void TestParsingHtmlInvalidGraphParsingWithoutCheckTest()
        {
            OpenGraph graph = OpenGraph.ParseHtml(this.invalidSampleContent);

            Assert.Equal(string.Empty, graph.Type);
            Assert.False(graph.Metadata.ContainsKey("mistake"));
            Assert.Equal("Product Title", graph.Title);
            Assert.Equal("http://www.test.com/test.png", graph.Image.ToString());
            Assert.Equal("http://www.test.com/", graph.Url.ToString());
            Assert.Equal("My Description", graph.Metadata["og:description"].First().Value);

            Assert.Equal("Test Site", graph.Metadata["og:site_name"].First().Value);
        }

        /// <summary>
        /// Tests the parsing amazon URL asynchronous test.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact]
        public async Task TestParsingAsyncTest()
        {
            OpenGraph graph = await OpenGraph.ParseUrlAsync(SpacedLink).ConfigureAwait(false);
            Assert.Contains("<html", graph.OriginalHtml, StringComparison.InvariantCultureIgnoreCase);
            this.AssertSpaced(graph);
        }

        /// <summary>
        /// Test parsing a URL.
        /// </summary>
        [Fact]
        public void TestParsingUrlTest()
        {
            OpenGraph graph = OpenGraph.ParseUrl(SpacedLink);
            Assert.Contains("<html", graph.OriginalHtml, StringComparison.InvariantCultureIgnoreCase);
            this.AssertSpaced(graph);
        }

        /// <summary>
        /// Tests the parsing of URLs without a scheme.
        /// </summary>
        /// <returns>A task that represents this async operation.</returns>
        [Fact]
        public async Task TestParsingUrlsWithoutScheme()
        {
            var withoutScheme = SpacedLink.Replace("http://", string.Empty, StringComparison.InvariantCultureIgnoreCase);
            var withHttpsScheme = SpacedLink.Replace("http", "https", StringComparison.InvariantCultureIgnoreCase);

            OpenGraph.ParseUrl(withHttpsScheme);
            await OpenGraph.ParseUrlAsync(withHttpsScheme).ConfigureAwait(false);

            OpenGraph.ParseUrl(withoutScheme);
            await OpenGraph.ParseUrlAsync(withoutScheme).ConfigureAwait(false);
        }

        /// <summary>
        /// Tests the parsing URL asynchronous validate encoding is correct.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact]
        public async Task TestParsingUrlAsyncValidateEncodingIsCorrect()
        {
            var expectedContent = "Создайте себе горное настроение с нашим первым фан-китом по игре #SteepGame&amp;#33; -&amp;gt; http://ubi.li/u8w9n";
            var tags = await OpenGraph.ParseUrlAsync("https://vk.com/wall-41600377_66756").ConfigureAwait(false);
            Assert.Equal(expectedContent, tags.Metadata["og:description"].First().Value);
        }

        /// <summary>
        /// Tests the parsing URL validate encoding is correct.
        /// </summary>
        [Fact]
        public void TestParsingUrlValidateEncodingIsCorrect()
        {
            var expectedContent = "Создайте себе горное настроение с нашим первым фан-китом по игре #SteepGame&amp;#33; -&amp;gt; http://ubi.li/u8w9n";
            var tags = OpenGraph.ParseUrl("https://vk.com/wall-41600377_66756");
            Assert.Equal(expectedContent, tags.Metadata["og:description"].First().Value);
        }

        /// <summary>
        /// Tests the meta charset parses correctly.
        /// </summary>
        [Fact]
        public void TestMetaCharsetParsesCorrectly()
        {
            var expectedTitle = "Réalité virtuelle : 360° de bonheur à améliorer";
            var expectedDescription = "Le cinéma à 360° a désormais son festival. Organisé par le Forum des images, le premier Paris Virtual Film Festival a donc vu le jour. Narration, réalisation, montage… une révolution balbutiante est en marche. Tour d&#039;horizon.";

            var ogs = OpenGraph.ParseUrl("http://www.telerama.fr/cinema/realite-virtuelle-360-de-bonheur-a-ameliorer,144339.php?utm_medium=Social&utm_source=Twitter&utm_campaign=Echobox&utm_term=Autofeed#link_time=1466595239");

            Assert.Equal(expectedTitle, ogs.Title);
            Assert.Equal(expectedDescription, ogs.Metadata["og:description"].First().Value);
        }

        /// <summary>
        /// Tests the URL decoding URL values.
        /// </summary>
        ////[Fact(Skip = "The sample site generates a new image URL with each request... can no longer use to test")]
        [Fact]
        public void TestUrlDecodingUrlValues()
        {
            var expectedUrl = "https://prod-video-us-west-2.pscp.tv/lXc5gSh6UPaWdc37LtVCb3UdtSfvj2QNutojPK2du5YWrNchfI4wXpwwHKTyfDhmfT2ibsBZV4doQeWlhSvI4A==/replay_thumbnail/us-west-2/periscope-replay-direct-live/eyJkIjowfQ/chunk_314.jpg?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsInZlcnNpb24iOiIyIn0.eyJBbGxvd2VkUHJvdG9jb2xzIjpbInRodW1iIl0sIkJyb2FkY2FzdElkIjoiMURYeHlaWlpWeWtLTSIsIkdyYW50VHlwZSI6InJlYWQiLCJHcmFudGVkQXQiOjE1NDQwNTgzMjgsIkdyYW50ZWRUbyI6IndlYi1yYXRlLWxpbWl0ZWQtMDA4NjkxZjE3ZDUyY2E3OTMyNTdlYWQ4NTkyOGE1N2IiLCJTdHJlYW1OYW1lIjoibFhjNWdTaDZVUGFXZGMzN0x0VkNiM1VkdFNmdmoyUU51dG9qUEsyZHU1WVdyTmNoZkk0d1hwd3dIS1R5ZkRobWZUMmlic0JaVjRkb1FlV2xoU3ZJNEE9PSIsImV4cCI6MTU0NDIzMTEyOH0.UbJfSn8A0BFBRJHAlhgBxAxGv4DuMnTnwGOztR-3swE&service=proxsee&digest=tGkjjGWVywQpODOMKx7vAqtCh-QohM9rSCR8KM_JtN4";
            var og = OpenGraph.ParseHtml(PeriscopeTvContent);

            Assert.Equal(expectedUrl, og.Image.ToString());
            Assert.Equal("320", og.Metadata["image"].First().Properties["width"].Value());
            Assert.Equal("568", og.Metadata["image"].First().Properties["height"].Value());
        }

        /// <summary>
        /// Defines the test method TestDownloader. Issue #15.
        /// </summary>
        /// <returns>A/an <c>Task</c>.</returns>
        [Fact]
        public async Task TestDownloader()
        {
            var downloader = new HttpDownloader(new Uri("https://marketplace.visualstudio.com/items?itemName=sdras.vue-vscode-extensionpack"), "test", "test");
            string html = await downloader.GetPageAsync().ConfigureAwait(false);

            Assert.NotEqual(string.Empty, html);
        }

        /// <summary>
        /// Defines the test method TestIssue14FromGit.
        /// </summary>
        /// <returns>A <c>Task</c>.</returns>
        [Fact]
        public async Task TestIssue14FromGit()
        {
            var urls = new List<Task<OpenGraph>>
            {
                OpenGraph.ParseUrlAsync("https://www.cntraveler.com/story/how-cruise-lines-are-getting-more-eco-friendly"),
                OpenGraph.ParseUrlAsync("https://www.nytimes.com/2019/05/22/travel/leaping-caimans-and-tasty-piranha-in-the-wild-amazon.html"),
                OpenGraph.ParseUrlAsync("https://www.thrillist.com/travel/nation/visiting-hainan-things-to-know"),
            };

            await Task.WhenAll(urls);
        }

        /// <summary>
        /// Asserts the spaced open graph data.
        /// </summary>
        /// <param name="graph">The graph.</param>
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void AssertSpaced(OpenGraph graph)
        {
            Assert.Equal(2, graph.Namespaces.Count);
            Assert.Equal("http://ogp.me/ns#", graph.Namespaces["og"].SchemaUri.ToString());
            Assert.Equal("og", graph.Namespaces["og"].Prefix);
            Assert.Equal("http://www.facebook.com/2008/fbml", graph.Namespaces["fb"].SchemaUri.ToString());
            Assert.Equal("fb", graph.Namespaces["fb"].Prefix);

            Assert.Equal(SpacedLink, graph.Url.ToString());
            Assert.StartsWith("Spaced (TV Series 1999–2001)", graph.Title, StringComparison.InvariantCultureIgnoreCase);
            Assert.Contains("Simon", graph.Metadata["og:description"].First().Value, StringComparison.InvariantCultureIgnoreCase);
            Assert.Contains("/images", graph.Image.ToString(), StringComparison.InvariantCultureIgnoreCase);
            Assert.Equal("video.tv_show", graph.Type);
            Assert.Equal("IMDb", graph.Metadata["og:site_name"].First().Value);

            Assert.Equal("115109575169727", graph.Metadata["fb:app_id"].First().Value);
        }
    }
}

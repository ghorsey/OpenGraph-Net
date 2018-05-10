namespace OpenGraphNet.Tests
{
    using OpenGraphNet.Metadata;
    using OpenGraphNet.Namespaces;

    using Xunit;

    public class MetaElementTests
    {
        private readonly Namespace ns; 

        public MetaElementTests()
        {
            this.ns = NamespaceRegistry.Instance.Namespaces["og"];
        }

        [Fact]
        public void TestNullMetaElement()
        {
            var element = new NullMetadata();

            Assert.Empty((string)element);
            Assert.Empty(element.ToString());
        }

        [Fact]
        public void TestMetaElement()
        {
            var element = new StructuredMetadata(this.ns, "title", "my title");
            
            Assert.Equal("my title", element);
            Assert.Equal(@"<meta property=""og:title"" content=""my title"">", element.ToString());
        }

        [Fact]
        public void TestStructuredElements()
        {
            var @expected = @"<meta property=""og:image"" content=""img1.png""><meta property=""og:image:height"" content=""30""><meta property=""og:image:width"" content=""60"">";

            var element = new StructuredMetadata(this.ns, "image", "img1.png");
            element.AddProperty(new PropertyMetadata("height", "30"));
            element.AddProperty(new PropertyMetadata("width", "60"));

            Assert.Equal("img1.png", element);
            Assert.Equal(expected, element.ToString());
        }

        [Fact]
        public void TestStructuredElementWithArrayProperty()
        {
            var @expected = @"<meta property=""og:locale"" content=""es""><meta property=""og:locale:alternate"" content=""es_ES""><meta property=""og:locale:alternate"" content=""es_US"">";
            var element = new StructuredMetadata(this.ns, "locale", "es");
            element.AddProperty(new PropertyMetadata("alternate", "es_ES"));
            element.AddProperty(new PropertyMetadata("alternate", "es_US"));

            Assert.Equal("es", element);
            Assert.Equal(expected, element.ToString());

            Assert.Equal("es", element.Value);
            Assert.Equal("es_ES", element.Properties["alternate"][0].Value);
            Assert.Equal("es_US", element.Properties["alternate"][1].Value);
        }
    }
}

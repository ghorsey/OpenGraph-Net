namespace OpenGraphNet.Framework472.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestClass]
    public class OpenGraphTests
    {
        [TestMethod]
        public async Task TestIssue14Urls()
        {
            var urls = new List<Task<OpenGraph>>
            {
                OpenGraph.ParseUrlAsync("https://www.cntraveler.com/story/how-cruise-lines-are-getting-more-eco-friendly"),
                ////OpenGraph.ParseUrlAsync("https://www.nytimes.com/2019/05/22/travel/leaping-caimans-and-tasty-piranha-in-the-wild-amazon.html"),
                ////OpenGraph.ParseUrlAsync("https://www.thrillist.com/travel/nation/visiting-hainan-things-to-know"),
            };

            await Task.WhenAll(urls);
        }
    }
}

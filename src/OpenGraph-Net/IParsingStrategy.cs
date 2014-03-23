using System.Collections.Generic;
using HtmlAgilityPack;

namespace OpenGraph_Net
{
    public interface IParsingStrategy
    {
        void Parse(IDictionary<string, string> openGraphData, HtmlDocument document);
    }
}
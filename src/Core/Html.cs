#region Copyright (c) 2016 Atif Aziz. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace WebLinq
{
    using Fizzler.Systems.HtmlAgilityPack;
    using HtmlAgilityPack;

    public interface IHtmlParser
    {
        IParsedHtml Parse(string html);
    }

    public interface IParsedHtml
    {
        string OuterHtml(string selector);
    }

    public sealed class HtmlParser : IHtmlParser
    {
        readonly QueryContext _context;

        public HtmlParser(QueryContext context)
        {
            _context = context;
        }

        public IParsedHtml Parse(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml2(html);
            return new ParsedHtml(doc);
        }

        sealed class ParsedHtml : IParsedHtml
        {
            readonly HtmlDocument _document;

            public ParsedHtml(HtmlDocument document)
            {
                _document = document;
            }

            public string OuterHtml(string selector) =>
                _document.DocumentNode.QuerySelector(selector)?.OuterHtml;

            public override string ToString() =>
                _document.DocumentNode.OuterHtml;
        }
    }
}
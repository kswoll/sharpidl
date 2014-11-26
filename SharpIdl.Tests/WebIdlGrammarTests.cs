﻿using NUnit.Framework;
using PEG;

namespace SharpIdl.Tests
{
    [TestFixture]
    public class WebIdlGrammarTests
    {
        [Test]
        public void Integer()
        {
            var parser = new PegParser(WebIdlGrammar.Instance, WebIdlGrammar.Instance.GetNonterminal(x => x.Integer()));
            Assert.NotNull(parser.ParseString("0"));
            Assert.NotNull(parser.ParseString("5"));
            Assert.NotNull(parser.ParseString("10"));
            Assert.NotNull(parser.ParseString("-5"));
            Assert.NotNull(parser.ParseString("-50"));
            Assert.NotNull(parser.ParseString("08"));
            Assert.NotNull(parser.ParseString("0x5"));
            Assert.NotNull(parser.ParseString("0xF1"));

            Assert.IsNull(parser.ParseString("0xGI"));
        }
    }
}
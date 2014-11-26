using NUnit.Framework;
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
            Assert.NotNull(parser.ParseString("10"));
            Assert.NotNull(parser.ParseString("-1"));
            Assert.NotNull(parser.ParseString("-10"));
            Assert.NotNull(parser.ParseString("-08"));
            Assert.NotNull(parser.ParseString("-0xF1"));

            Assert.IsNull(parser.ParseString("0xGI"));
            Assert.IsNull(parser.ParseString("9A"));
            Assert.IsNull(parser.ParseString("09"));
        }

        [Test]
        public void Float()
        {
            var parser = new PegParser(WebIdlGrammar.Instance, WebIdlGrammar.Instance.GetNonterminal(x => x.Float()));
            Assert.NotNull(parser.ParseString("1."));
            Assert.NotNull(parser.ParseString(".1"));
            Assert.NotNull(parser.ParseString("1.1"));
            Assert.NotNull(parser.ParseString("-1.1"));
            Assert.NotNull(parser.ParseString("-.1"));
            Assert.NotNull(parser.ParseString("-1."));
            Assert.NotNull(parser.ParseString("1e+10"));
            Assert.IsNull(parser.ParseString("."));
        }

        [Test]
        public void Identifier()
        {
            var parser = new PegParser(WebIdlGrammar.Instance, WebIdlGrammar.Instance.GetNonterminal(x => x.Identifier()));
            Assert.NotNull(parser.ParseString("abcd"));
            Assert.NotNull(parser.ParseString("a1"));
            Assert.NotNull(parser.ParseString("AEr3"));
            Assert.IsNull(parser.ParseString("1a"));
        }

        [Test]
        public void String()
        {
            var parser = new PegParser(WebIdlGrammar.Instance, WebIdlGrammar.Instance.GetNonterminal(x => x.String()));
            Assert.NotNull(parser.ParseString("\"a\""));
        }

        [Test]
        public void Whitespace()
        {
            var parser = new PegParser(WebIdlGrammar.Instance, WebIdlGrammar.Instance.GetNonterminal(x => x.Whitespace()));
            Assert.NotNull(parser.ParseString("\n"));            
            Assert.NotNull(parser.ParseString("\r"));            
            Assert.NotNull(parser.ParseString("\r\n"));            
            Assert.NotNull(parser.ParseString(" "));            
            Assert.NotNull(parser.ParseString("\t"));            
        }
    }
}
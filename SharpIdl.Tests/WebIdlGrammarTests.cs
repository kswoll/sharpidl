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
            var parser = new PegParser(WebIdlGrammar.Instance, WebIdlGrammar.Instance.GetNonterminal(x => x.WS()));
            Assert.NotNull(parser.ParseString("\n"));            
            Assert.NotNull(parser.ParseString("\r"));            
            Assert.NotNull(parser.ParseString("\r\n"));            
            Assert.NotNull(parser.ParseString(" "));            
            Assert.NotNull(parser.ParseString("\t"));            
            Assert.NotNull(parser.ParseString(" // asdfasdfs"));            
            Assert.NotNull(parser.ParseString(" // asdfasdfs\r\n"));            
            Assert.NotNull(parser.ParseString(" /* asdf */ "));
        }

        [Test]
        public void Other()
        {
            var parser = new PegParser(WebIdlGrammar.Instance, WebIdlGrammar.Instance.GetNonterminal(x => x.Other()));
            Assert.NotNull(parser.ParseString("-1"));
            Assert.NotNull(parser.ParseString("53"));
            Assert.NotNull(parser.ParseString("1.534"));
            Assert.NotNull(parser.ParseString("abcd"));
            Assert.NotNull(parser.ParseString("\"abcd\""));
            Assert.NotNull(parser.ParseString("-"));
            Assert.NotNull(parser.ParseString("."));
            Assert.NotNull(parser.ParseString("..."));
            Assert.NotNull(parser.ParseString(":"));
            Assert.NotNull(parser.ParseString(";"));
            Assert.NotNull(parser.ParseString("<"));
            Assert.NotNull(parser.ParseString(">"));
            Assert.NotNull(parser.ParseString("="));
            Assert.NotNull(parser.ParseString("?"));
            Assert.NotNull(parser.ParseString("Date"));
            Assert.NotNull(parser.ParseString("DOMString"));
            Assert.NotNull(parser.ParseString("Infinity"));
            Assert.NotNull(parser.ParseString("NaN"));
            Assert.NotNull(parser.ParseString("any"));
            Assert.NotNull(parser.ParseString("boolean"));
            Assert.NotNull(parser.ParseString("byte"));
            Assert.NotNull(parser.ParseString("double"));
            Assert.NotNull(parser.ParseString("false"));
            Assert.NotNull(parser.ParseString("float"));
            Assert.NotNull(parser.ParseString("long"));
            Assert.NotNull(parser.ParseString("null"));
            Assert.NotNull(parser.ParseString("object"));
            Assert.NotNull(parser.ParseString("octet"));
            Assert.NotNull(parser.ParseString("or"));
            Assert.NotNull(parser.ParseString("optional"));
            Assert.NotNull(parser.ParseString("sequence"));
            Assert.NotNull(parser.ParseString("short"));
            Assert.NotNull(parser.ParseString("true"));
            Assert.NotNull(parser.ParseString("unsigned"));
            Assert.NotNull(parser.ParseString("void"));
            
        }
    }
}
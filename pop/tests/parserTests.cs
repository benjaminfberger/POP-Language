using NUnit.Framework;
using src.parsing;
using src.parsing.ast;
using System.Text.RegularExpressions;

namespace tests
{
    [TestFixture]
    public class parserTests
    {
        private parser parser;
        [Test]
        public void parseShouldParseEmptyListOfTokens()
        {
            parser = new parser(new List<token> { new token(tokenType.eof, "eof", 0) });
            List<iProgramNode> actual = parser.parse();
            List<iProgramNode> expected = new List<iProgramNode>();
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void parseShouldReturnAstContainingTerminalNodeForStringLiteral()
        {
            parser = new parser(new List<token> { new token(tokenType.stringLiteral, "Hello, World!", 0) });
            List<iProgramNode> actual = parser.parse();
            List<iProgramNode> expected = new List<iProgramNode> 
            {
                new terminalNode("\"Hello, World!\"")
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}

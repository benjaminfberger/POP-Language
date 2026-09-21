using src.parsing;
using src.parsing.ast;

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
            List<token> tokens = new List<token>
            {
                new token(tokenType.stringLiteral, "\"Hello, World!\"", 0),
                new token(tokenType.eof, "eof", 0)
            };
            parser = new parser(tokens);
            List<iProgramNode> actual = parser.parse();

            Assert.That(actual.Count, Is.EqualTo(1));
            Assert.That(actual[0], Is.InstanceOf<terminalNode>());

            terminalNode terminal = actual[0] as terminalNode;
            Assert.That(terminal.value, Is.EqualTo("\"Hello, World!\""));
        }

    }
}

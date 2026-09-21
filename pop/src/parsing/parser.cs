using src.parsing.ast;
using System.Security;
using System.Text.RegularExpressions;

namespace src.parsing
{
    public class parser
    {
        private readonly List<token> tokens;
        private List<iProgramNode> ast;
        private int pointer;
        public parser(List<token> tokens)
        {
            pointer = 0;
            ast = new List<iProgramNode>();
            this.tokens = tokens;
        }
        private token peek() => pointer + 1 < tokens.Count ? 
            tokens[pointer + 1] : 
            tokens[tokens.Count - 1];
        private void consume() => pointer++;
        private bool match(tokenType type)
        {
            if (peek().type == type) return true;
            throw new parserException($"Unexpected token. Expected {type.ToString()}. Found {peek().type}.");
        }
        public List<iProgramNode> parse()
        {
            while (peek().type != tokenType.eof)
                ast.Add(parseExpression());

            return ast;
        }
        public iProgramNode parseExpression()
        {
            token current = peek();
            switch (current.type)
            {
                case tokenType.leftBrace:
                    consume();
                    List<iProgramNode> children = new List<iProgramNode>();
                    while (peek().type != tokenType.rightBrace)
                        children.Add(parseExpression());
                    match(tokenType.rightBrace);
                    return new invokeNode("block", new List<iProgramNode>(), children);
                default:
                    return parseTerminal();
            }
        }
        public iProgramNode parseTerminal()
        {
            token current = peek();
            switch (current.type)
            {
                case tokenType.stringLiteral:
                    consume();
                    return new terminalNode(current.value);
                case tokenType.identifier:
                    consume();
                    return new terminalNode(current.value);
                case tokenType.leftParentheses:
                    consume();
                    iProgramNode children = parseExpression();
                    match(tokenType.rightParentheses);
                    return children;
                default:
                    throw new parserException($"Unexpected token. Found {current.type.ToString()}");
            }
        }
    }
}

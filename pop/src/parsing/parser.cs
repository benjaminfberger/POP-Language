using src.parsing.ast;

namespace src.parsing
{
    public class parser
    {
        private readonly List<token> tokens;
        private readonly List<iProgramNode> ast;
        private int pointer;

        public parser(List<token> tokens)
        {
            this.pointer = 0;
            this.ast = new List<iProgramNode>();
            this.tokens = tokens;
        }

        private token peek() => pointer < tokens.Count ? tokens[pointer] : tokens[tokens.Count - 1];

        private void consume() => pointer++;

        private void match(tokenType type)
        {
            if (peek().type == type)
            {
                consume();
                return;
            }
            throw new parserException($"Unexpected token. Expected {type}. Found {peek().type}.");
        }

        public List<iProgramNode> parse()
        {
            while (peek().type != tokenType.eof)
            {
                ast.Add(parseExpression());
            }
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

                    while (peek().type != tokenType.rightBrace && peek().type != tokenType.eof)
                    {
                        children.Add(parseExpression());
                    }

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
                case tokenType.identifier:
                    consume();
                    return new terminalNode(current.value);

                case tokenType.leftParentheses:
                    consume();
                    iProgramNode expression = parseExpression();
                    match(tokenType.rightParentheses);
                    return expression;

                default:
                    throw new parserException($"Unexpected token. Found {current.type}");
            }
        }
    }
}

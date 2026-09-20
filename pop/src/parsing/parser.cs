using src.parsing.ast;

namespace src.parsing
{
    public class parser
    {
        private readonly List<token> tokens;
        private List<iProgramNode> ast;
        private int pointer;
        public parser(List<token> tokens)
        {
            ast = new List<iProgramNode>();
            this.tokens = tokens;
        }
        private token peek() => tokens[pointer + 1];
        private token consume() => tokens[pointer++];
        public List<iProgramNode> parse()
        {
            pointer = 0;
            token current;
            while (true)
            {
                current = tokens[pointer];

                consume();
            }
            throw new NotImplementedException();
        }
        public iProgramNode parseNext()
        {
            token current = peek();
            switch (current.type)
            {
                case tokenType.stringLiteral:
                    return new terminalNode(current.value);
                case tokenType.leftParentheses:
                    consume();
                    while (peek().type != tokenType.rightParentheses)
                    {
                        // TODO: finish
                    }
                    break;
            }
            throw new NotImplementedException();
        }
    }
}

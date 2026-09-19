using src.parsing.ast;

namespace src.parsing
{
    public class parser
    {
        private List<token> tokens;
        private List<iProgramNode> ast;
        private int pointer;
        public parser(List<token> tokens)
        {
            ast = new List<iProgramNode>();
            this.tokens = tokens;
        }
        private token peek() => tokens[pointer + 1];
        private void consume() => pointer++;
        public List<iProgramNode> parse()
        {
            pointer = 0;
            throw new NotImplementedException();
        }
    }
}

using System.Transactions;

namespace src.parsing
{
    public class tokenizer
    {
        private string source;
        public tokenizer(string source) => this.source = source;
        public List<token> tokenize()
        {
            List<token> tokens = new();
            int currentIndex = 0;
            int startIndex = 0;
            int line = 0;
            do
            {
                char current = source[currentIndex];
                startIndex = currentIndex;
                currentIndex++;



                switch (current)
                {
                    case ' ':
                    case '\t':
                        break;

                    case '\n':
                        line++;
                        break;

                    case '{':
                        tokens.Add(new token(tokenType.leftBrace, "{", line));
                        break;
                    case '}':
                        tokens.Add(new token(tokenType.rightBrace, "}", line));
                        break;
                    case ';':
                        tokens.Add(new token(tokenType.semiColon, ";", line));
                        break;
                    case '=':
                        tokens.Add(new token(tokenType.equals, "=", line));
                        break;

                    default:
                        break;
                }

            } while (currentIndex < source.Length);


            return tokens;
        }
    }
}
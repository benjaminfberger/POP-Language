using pop.parsing;

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
            while (currentIndex < source.Length)
            {
                char current = source[currentIndex];
                startIndex = currentIndex;
                currentIndex++;

                switch (current)
                {
                    case ' ':
                    case '\t':
                    case '\0':
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
                        if (char.IsLetter(current) || current == '_')
                        {
                            while (currentIndex < source.Length &&
                                (char.IsLetterOrDigit(source[currentIndex]) ||
                                source[currentIndex] == '_'))
                                currentIndex++;

                            string word = source[startIndex..currentIndex];
                            if (word != "eof")
                            {
                                tokenType tokenType = keywords.table.TryGetValue(word, out var type) ? type : tokenType.identity;
                                tokens.Add(new token(tokenType, word, line));
                            }
                        }
                        else
                            throw new tokenizationException("unknown character", line, current.ToString());
                        break;
                }

            }

            tokens.Add(new token(tokenType.eof, "eof", line));

            return tokens;
        }
    }
}
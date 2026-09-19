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
                    case '\r':
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
                    case '(':
                        tokens.Add(new token(tokenType.leftParentheses, "(", line));
                        break;
                    case ')':
                        tokens.Add(new token(tokenType.rightParentheses, ")", line));
                        break;

                    case '=':
                        tokens.Add(new token(tokenType.equals, "=", line));
                        break;

                    case '"':
                        while (currentIndex < source.Length &&
                            source[currentIndex] != '"')
                            if (source[currentIndex] == '\\' &&
                                currentIndex + 1 < source.Length &&
                                source[currentIndex + 1] == '"')
                                currentIndex += 2;
                            else
                                currentIndex++;

                        currentIndex++;

                        if (currentIndex >= source.Length)
                            throw new ArgumentOutOfRangeException();
                        tokens.Add(new token(tokenType.stringLiteral, source[startIndex..currentIndex], line));
                        break;

                    case '/':
                        if (currentIndex < source.Length &&
                            source[currentIndex] == '/')
                        {
                            currentIndex += 2;
                            while (currentIndex < source.Length &&
                                source[currentIndex] != '\n')
                                currentIndex++;
                        }
                        else
                            tokens.Add(new token(tokenType.divide, "/", line));
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
                                tokenType tokenType = keywords.table.TryGetValue(word, out var type)
                                    ? type : tokenType.identifier;
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
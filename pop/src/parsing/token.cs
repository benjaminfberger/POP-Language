namespace src.parsing
{
    public record token
    {
        public tokenType type;
        public string value;
        public int line;
        public token(tokenType type, string value, int line)
        {
            this.type = type;
            this.value = value;
            this.line = line;
        }
    }
}
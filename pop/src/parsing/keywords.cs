namespace src.parsing
{
    public static class keywords
    {
        public static readonly Dictionary<string, tokenType> table = new Dictionary<string, tokenType>{
            { "identify", tokenType.identify },
            { "identity", tokenType.identity },
            { "identifier", tokenType.identifier },
            { "let", tokenType.let },
            { "sudo", tokenType.sudo },
            { "program", tokenType.program },
            { "init", tokenType.init },
            { "run", tokenType.run },
            { "terminate", tokenType.terminate },
            { "warn", tokenType.warn },
            { "{", tokenType.leftBrace },
            { "}", tokenType.rightBrace },
            { "=", tokenType.equals },
            { "halt", tokenType.halt },
            { "eof", tokenType.eof }
        };
    }
}
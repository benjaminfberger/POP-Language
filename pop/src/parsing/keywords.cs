using src.parsing;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace src.parsing
{
    public static class keywords
    {
        public static readonly Dictionary<string, tokenType> table = new Dictionary<string, tokenType>{
            { "identify", tokenType.identify },
            { "let", tokenType.let }, 
            { "sudo", tokenType.sudo },
            { "program", tokenType.program },
            { "init", tokenType.init },
            { "run", tokenType.run },
            { "terminate", tokenType.terminate },
            { "warn", tokenType.warn },
            { "{", tokenType.leftBrace },
            { "}", tokenType.rightBrace },
            { ";", tokenType.semiColon },
            { "=", tokenType.equals }
        };
    }
}
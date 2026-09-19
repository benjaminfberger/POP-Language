namespace src.parsing
{
    public class tokenizationException : Exception
    {
        public int line;
        public string message;
        public tokenizationException(string cause, int line, string error = "")
            : base($"{cause} at line {line}. Found: '{error}'")
        {
            this.line = line;
            this.message = error;
        }

    }
}

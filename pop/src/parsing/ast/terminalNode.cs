namespace src.parsing.ast
{
    /// <summary>
    /// program that represents a literal or a value, has no body or parameters
    /// </summary>
    public record terminalNode(string value)
        : iProgramNode(value, new List<iProgramNode>(), new List<iProgramNode>());

}

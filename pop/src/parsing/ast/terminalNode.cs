namespace src.parsing.ast
{
    /// <summary>
    /// program that represents a literal or a value, has no body or parameters
    /// </summary>
    public record terminalNode(string identifier, iProgramNode @return)
        : iProgramNode(identifier, new List<iProgramNode>(), new List<iProgramNode>(), @return);

}

namespace src.parsing.ast
{
    /// <summary>
    /// program that executes instructions or routes data, has no return type
    /// </summary>
    internal record invokeNode(string identifier, List<iProgramNode> parameters, List<iProgramNode> body)
        : iProgramNode(identifier, parameters, body);
}

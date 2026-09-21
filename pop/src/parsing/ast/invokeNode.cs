namespace src.parsing.ast
{
    /// <summary>
    /// program that executes instructions or routes data, has no return type
    /// </summary>
    internal record invokeNode(string value, List<iProgramNode> parameters, List<iProgramNode> children)
        : iProgramNode(value, parameters, children);
}

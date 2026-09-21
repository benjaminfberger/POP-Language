namespace src.parsing.ast
{
    public abstract record iProgramNode(
        string value,
        List<iProgramNode> parameters,
        List<iProgramNode> children
    );
}
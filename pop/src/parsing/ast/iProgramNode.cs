namespace src.parsing.ast
{
    public abstract record iProgramNode(
        string identifier,
        List<iProgramNode> parameters,
        List<iProgramNode> body
    );
}
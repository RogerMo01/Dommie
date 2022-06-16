namespace DominoLibrary;

public interface IPlayer
{
    public string Name { get; }
    public List<IStrategy> Strategies { get; }
    Token_onBoard Play(Board board, List<Token> tokens);
}
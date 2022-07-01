namespace DominoLibrary;

public interface IStrategy
{
    Token_onBoard Play(Board board, List<Token> tokens, IPlayer player);
    string Info { get; }
}
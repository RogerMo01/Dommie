namespace DominoLibrary;

public interface IStrategy
{
    Token_onBoard Play(BoardInfo info, List<Token> tokens, IPlayer player);
}
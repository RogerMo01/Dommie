namespace DominoLibrary;

public interface IPlayer
{
    public string Name { get; }
    public IStrategy Strategy { get; }
    Token_onBoard Play(BoardInfo info ,List<Token> tokens, HumanPlayerMenu menu);
}
namespace DominoLibrary;

public delegate Token_onBoard HumanPlayerMenu(IPlayer player);

public interface IPlayer
{
    public string Name { get; }
    public IStrategy Strategy { get; }
    Token_onBoard Play(Board board, List<Token> tokens, HumanPlayerMenu humanPlayerMenu);
}
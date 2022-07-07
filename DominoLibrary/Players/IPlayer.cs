namespace DominoLibrary;

public delegate Token_onBoard HumanPlayerMenu(IPlayer player);

public interface IPlayer
{
    public string Name { get; }
    public List<IStrategy> Strategies { get; }
    Token_onBoard Play(Board board, List<Token> tokens, HumanPlayerMenu humanPlayerMenu);
    public ConsoleColor Color { get; }
}
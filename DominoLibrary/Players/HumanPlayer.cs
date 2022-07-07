using ConsoleApp;
namespace DominoLibrary;

public class HumanPlayer : IPlayer
{
    public string Name { get; } = "You";

    public List<IStrategy> Strategies { get; }

    public ConsoleColor Color { get; }


    public HumanPlayer(List<IStrategy> strategies, ConsoleColor color)
    {
        Strategies = strategies;
        Color = color;
    }

    public Token_onBoard Play(Board board, List<Token> tokens, HumanPlayerMenu humanPlayerMenu)
    {
        return humanPlayerMenu(this);
    }

    public override string ToString() => Name;
}
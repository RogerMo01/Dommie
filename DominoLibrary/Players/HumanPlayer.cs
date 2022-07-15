using ConsoleApp;
namespace DominoLibrary;

public class HumanPlayer : IPlayer
{
    public string Name { get; } = "You";

    public IStrategy Strategy { get; }

    public ConsoleColor Color { get; }


    public HumanPlayer(IStrategy strategies, ConsoleColor color)
    {
        Strategy = strategies;
        Color = color;
    }

    public Token_onBoard Play(Board board, List<Token> tokens, HumanPlayerMenu humanPlayerMenu)
    {
        return humanPlayerMenu(this);
    }

    public override string ToString() => Name;
}
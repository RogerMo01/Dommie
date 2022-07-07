namespace DominoLibrary;

public class Player : IPlayer
{
    public string Name { get; }
    public List<IStrategy> Strategies { get; }
    public ConsoleColor Color { get; }

    public Player(string name, List<IStrategy> strategies, ConsoleColor color)
    {
        Name = name;
        Strategies = strategies;
        Color = color;
    }

    public Token_onBoard Play(Board board, List<Token> tokens, HumanPlayerMenu humanPlayerMenu)
    {
        return Strategies[0].Play(board, tokens, this);
    }

    public override string ToString()
    {
        return this.Name;
    }
}
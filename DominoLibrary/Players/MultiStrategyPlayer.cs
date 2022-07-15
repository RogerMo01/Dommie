namespace DominoLibrary;

public class MultiStrategyPlayer : IPlayer
{
    public string Name { get; }
    public List<IStrategy> Strategies { get; }
    public ConsoleColor Color { get; }

    public MultiStrategyPlayer(string name, List<IStrategy> strategies, ConsoleColor color)
    {
        Name = name;
        Strategies = strategies;
        Color = color;
    }

    public Token_onBoard Play(Board board, List<Token> tokens, HumanPlayerMenu humanPlayerMenu)
    {
        Random random = new Random();
        return Strategies[random.Next(Strategies.Count)].Play(board, tokens, this);
    }

    public override string ToString()
    {
        return this.Name;
    }
}
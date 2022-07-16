namespace DominoLibrary;

public class SingleStrategyPlayer : IPlayer
{
    public string Name { get; }
    public IStrategy Strategy { get; }
    public ConsoleColor Color { get; }

    public SingleStrategyPlayer(string name, IStrategy strategies, ConsoleColor color)
    {
        Name = name;
        Strategy = strategies;
        Color = color;
    }

    public Token_onBoard Play(Board board, List<Token> tokens, HumanPlayerMenu humanPlayerMenu)
    {
        return Strategy.Play(board, tokens, this);
    }

    public override string ToString()
    {
        return this.Name;
    }
}
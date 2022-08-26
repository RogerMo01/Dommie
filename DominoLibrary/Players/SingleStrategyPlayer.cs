namespace DominoLibrary;

public class SingleStrategyPlayer : IPlayer
{
    public string Name { get; }
    public IStrategy Strategy { get; }

    public SingleStrategyPlayer(string name, IStrategy strategies)
    {
        Name = name;
        Strategy = strategies;
    }

    public Token_onBoard Play(BoardInfo info, List<Token> tokens, HumanPlayerMenu menu)
    {
        return Strategy.Play(info, tokens, this);
    }

    public override string ToString()
    {
        return this.Name;
    }
}
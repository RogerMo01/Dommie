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

    public Token_onBoard Play(Board board, List<Token> tokens, HumanPlayerMenu humanPlayerMenu)
    {
        return Strategy.Play(board, tokens, this);
    }

    public override string ToString()
    {
        return this.Name;
    }
}
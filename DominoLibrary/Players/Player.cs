namespace DominoLibrary;

public class Player : IPlayer
{
    public string Name { get; }
    public List<IStrategy> Strategies { get; }

    public Player(string name, List<IStrategy> strategies)
    {
        Name = name;
        Strategies = strategies;
    }

    public Token_onBoard Play(Board board, List<Token> tokens)
    {
        return Strategies[0].Play(board, tokens, this);
    }

    public override string ToString()
    {
        return this.Name;
    }
}
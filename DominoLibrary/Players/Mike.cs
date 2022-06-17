namespace DominoLibrary;

public class Mike : IPlayer
{
    public string Name {get; }
    public List<IStrategy> Strategies {get; }

    public Mike(List<IStrategy> strategies)
    {
        Name = "Mike";
        Strategies = strategies;
    }

    public Mike(string newName, List<IStrategy> strategies)
    {
        Name = newName;
        Strategies = strategies;
    }
    public Token_onBoard Play(Board board, List<Token> tokens)
    {
        return Strategies[1].Play(board, tokens, this);
    }
}
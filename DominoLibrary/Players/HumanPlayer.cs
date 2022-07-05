
namespace DominoLibrary;

public class HumanPlayer : IPlayer
{
    public string Name { get; }

    public List<IStrategy> Strategies { get; }

    public Token_onBoard Play(Board board, List<Token> tokens)
    {
        
    }

    public HumanPlayer(string name, List<IStrategy> strategies)
    {
        Name = name;
        Strategies = strategies;
    }


}
namespace DominoLibrary;

public delegate Token_onBoard HumanPlayerMenu(BoardInfo info, List<Token> tokens, IPlayer player);

public class HumanPlayer : IPlayer
{
    public string Name { get; } = "You";
    public IStrategy Strategy { get; }

    public HumanPlayer(IStrategy strategies)
    {
        Strategy = strategies;
    }

    public Token_onBoard Play(BoardInfo info, List<Token> tokens, HumanPlayerMenu menu)
    {
        return menu(info, tokens, this);
    }
    
    public override string ToString() => Name;
}
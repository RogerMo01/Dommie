using Utils;

namespace DominoLibrary;

public class Setting
{
    public CircularList<IPlayer> Players { get; private set; }
    public IGameOver OverCondition { get; private set; }
    public int MaxToken { get; private set; }
    public Node<IPlayer> Inner { get; private set; }

    public Setting(CircularList<IPlayer> players, IGameOver overCondition, int maxToken, Node<IPlayer> inner)
    {
        Players = players;
        OverCondition = overCondition;
        MaxToken = maxToken;
        Inner = inner;
    }
}
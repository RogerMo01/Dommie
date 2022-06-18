using Utils;

namespace DominoLibrary;

public class Setting
{
    public CircularList<IPlayer> Players { get; private set; }
    public int MaxToken { get; private set; }
    public Node<IPlayer> Inner { get; private set; }
    public GamePrinter GamePrinter { get; private set; }

    public Setting(CircularList<IPlayer> players, int maxToken, Node<IPlayer> inner, GamePrinter gamePrinter)
    {
        Players = players;
        MaxToken = maxToken;
        Inner = inner;
        GamePrinter = gamePrinter;
    }
}
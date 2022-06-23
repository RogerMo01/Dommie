using Utils;

namespace DominoLibrary;

public class BoardSetting
{
    public CircularList<IPlayer> Players { get; private set; }
    public Node<IPlayer> Inner { get; private set; }
    public List<Token> GameTokens { get; private set; }
    public int TokensPerPlayer { get; private set; }
    public WinBoard WinBoard { get; private set; }
    public WinnerBoard WinnerBoard {get; private set; }

    public BoardSetting(CircularList<IPlayer> players, Node<IPlayer> inner, List<Token> gameTokens, int tokensPerPlayer, WinBoard winB, WinnerBoard winnerB)
    {
        Players = players;
        GameTokens = gameTokens;
        Inner = inner;
        TokensPerPlayer = tokensPerPlayer;
        WinBoard = winB;
        WinnerBoard = winnerB;
    }
}

public class TournamentSetting
{
    public CircularList<IPlayer> Players { get; private set; }
    public int MaxToken { get; private set; }
    public int WinScore { get; private set; }
    public WinBoard WinBoard { get; private set; }
    public WinnerBoard WinnerBoard {get; private set; }

    public TournamentSetting(CircularList<IPlayer> players, int maxToken, int winScore, WinBoard winB, WinnerBoard winnerB)
    {
        Players = players;
        MaxToken = maxToken;
        WinScore = winScore;
        WinBoard = winB;
        WinnerBoard = winnerB;
    }
}
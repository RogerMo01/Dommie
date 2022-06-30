using Utils;
namespace DominoLibrary;

public abstract class Setting
{
    public CircularList<IPlayer>? Players { get; set; }
    public Judge? Judge { get; set; }

}
public class BoardSetting : Setting
{
    public Node<IPlayer> Inner { get; private set; }
    public List<Token> GameTokens { get; private set; }
    public int TokensPerPlayer { get; private set; }

    public BoardSetting(CircularList<IPlayer> players, Node<IPlayer> inner, List<Token> gameTokens, int tokensPerPlayer, Judge judge)
    {
        Players = players;
        GameTokens = gameTokens;
        Inner = inner;
        TokensPerPlayer = tokensPerPlayer;
        Judge = judge;
    }
}

public class TournamentSetting : Setting
{
    public int MaxToken { get; private set; }
    public int WinScore { get; private set; }
    public int TotalPlayers {get; private set; }

    public TournamentSetting(CircularList<IPlayer> players, int maxToken, int totalPlayers, int winScore, Judge judge)
    {
        Players = players;
        MaxToken = maxToken;
        TotalPlayers = totalPlayers;
        WinScore = winScore;
        Judge = judge;
    }
}
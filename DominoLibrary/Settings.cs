using Utils;
namespace DominoLibrary;

public abstract class Setting
{
    public CircularList<IPlayer>? Players { get; set; }
    public Judge? Judge { get; set; }
    public List<Team>? Teams { get; set; }
    public int MaxToken { get; set; }
    public HumanPlayerMenu? HumanMenu { get; set; }
}

public class RoundSetting : Setting
{
    public List<Token> GameTokens { get; private set; }
    public int TokensPerPlayer { get; private set; }

    public RoundSetting(CircularList<IPlayer> players, List<Token> gameTokens, int tokensPerPlayer, Judge judge, List<Team> teams, int maxToken, HumanPlayerMenu humanMenu)
    {
        Players = players;
        GameTokens = gameTokens;
        TokensPerPlayer = tokensPerPlayer;
        Judge = judge;
        Teams = teams;
        MaxToken = maxToken;
        HumanMenu = humanMenu;
    }
}

public class TournamentSetting : Setting
{
    public int WinScore { get; private set; }
    public int TotalPlayers {get; private set; }

    public TournamentSetting(CircularList<IPlayer> players, int maxToken, int totalPlayers, int winScore, Judge judge, List<Team> team, HumanPlayerMenu humanMenu)
    {
        Players = players;
        TotalPlayers = totalPlayers;
        WinScore = winScore;
        Judge = judge;
        Teams = team;
        MaxToken = maxToken;
        HumanMenu = humanMenu;
    }
}
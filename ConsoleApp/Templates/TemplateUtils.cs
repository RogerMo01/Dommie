using DominoLibrary;
using Utils;
namespace ConsoleApp;

public interface ITemplate
{
    public Round Round { get; }
    public Tournament Tournament { get; }
    public string Title { get; }
}

public static class TemplateUtils
{
    public static CircularList<IPlayer> ToCircularList(List<IPlayer> playersList)
    {
        CircularList<IPlayer> players = new CircularList<IPlayer>(playersList[0]);
        for (int i = 1; i < playersList.Count; i++)
        {
            players.AddLast(playersList[i]);
        }
        return players;
    }
    public static List<IPlayer> GeneratePlayers(int numberPlayers, List<IStrategy> strategies)
    {
        List<IPlayer> players = new List<IPlayer>();
        List<string> usedNames = new List<string>();

        for (int i = players.Count; i < numberPlayers; i++)
        {
            players.Add(GetRandomPlayer(ref usedNames, strategies));
        }

        return players;
    }

    public static List<Team> AssignTeamsClassic(List<IPlayer> players)
    {
        List<Team> teams = new List<Team>(){ new Team(new List<IPlayer>()), new Team(new List<IPlayer>())};

        for (int i = 0; i < players.Count; i++)
        {
            if(i %2 == 0)
            {
                teams[0].PlayersTeam.Add(players[i]);
            }
            else
            {
                teams[1].PlayersTeam.Add(players[i]);
            }
        }

        return teams;
    }

    public static IPlayer GetRandomPlayer(ref List<string> usedNames, List<IStrategy> strategies)
    {
        string[] names = // se pueden poner m'as
        {
            "Eleven", "Mike", "Dustin", "Jane", "Hooper",
            "Will", "Lucas", "Joyce", "Nancy", "Steve",
            "Jonathan", "Max", "Vecna", "Mindflyer", "Bob",
            "Demogorgon", "Erika", "Eddie", "Billy"
        };

        Random r = new Random();
        int strategyChoice = r.Next(2);

        int nameChoice = 0;
        bool repeated = true;
        while (repeated)
        {
            nameChoice = r.Next(names.Length);
            if(!usedNames.Contains(names[nameChoice]))
            {
                usedNames.Add(names[nameChoice]);
                repeated = false;
            }
        }
        
        return new SingleStrategyPlayer(names[nameChoice], strategies[strategyChoice]);
    }

    public static ITemplate BuildTemplate(CircularList<IPlayer> players, int maxToken, int numberPlayers, int score, OverRound winB, WinnerRoundGetter winnerB, PointsGetter pointsGetter, InnerGetter innerSelect, List<Team> teams, bool humanPlay, HandOut handOut, HumanPlayerMenu humanMenu)
    {
        Judge judge = new Judge(winB, winnerB, pointsGetter, innerSelect, handOut);

        List<Token> gameTokens = Utils.Utils.GenerateTokens(maxToken);

        TournamentSetting tS = new TournamentSetting(players, maxToken, numberPlayers, score, judge, teams, humanMenu);
        Tournament t = new Tournament(tS);

        RoundSetting rS = new RoundSetting(players, gameTokens, Utils.Utils.DecideTokensPerPlayer(gameTokens.Count, players.Count), judge, teams, maxToken, humanMenu);
        Round r = new Round(rS);

        return new CustomTemplate(r, t);
    }
}

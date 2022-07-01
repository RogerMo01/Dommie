using DominoLibrary;
using Utils;
namespace ConsoleApp;

public interface ITemplate
{
    public Board Board { get; }
    public Tournament Tournament { get; }
    public string Title { get; }
}

static class TemplateUtils
{
    private static CircularList<IPlayer> ToCircularList(List<IPlayer> playersList)
    {
        CircularList<IPlayer> players = new CircularList<IPlayer>(playersList[0]);
        for (int i = 1; i < playersList.Count; i++)
        {
            players.AddLast(playersList[i]);
        }
        return players;
    }
    public static CircularList<IPlayer> GeneratePlayers(int numberPlayers, List<IStrategy> strategies)
    {
        List<IPlayer> players = new List<IPlayer>();
        List<string> usedNames = new List<string>();

        for (int i = players.Count; i < numberPlayers; i++)
        {
            players.Add(GetRandomPlayer(ref usedNames, strategies));
        }

        return ToCircularList(players);
    }

    public static IPlayer GetRandomPlayer(ref List<string> usedNames, List<IStrategy> strategies)
    {
        string[] names = new string[] // se pueden poner m'as
        {
            "Eleven", "Mike", "Dustin", "Jane", "Hooper",
            "Will", "Lucas", "Joyce", "Nancy", "Steve",
            "Jonathan", "Max", "Vecna", "Mindflyer", "Bob",
            "Demogorgon", "Erika", "Eddie"
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
        
        return new Player(names[nameChoice], new List<IStrategy>(){ strategies[strategyChoice] });
    }

    public static ITemplate BuildTemplate(CircularList<IPlayer> players, int maxToken, int score, WinBoard winB, WinnerBoard winnerB)
    {
        Judge judge = new Judge(winB, winnerB);

        TournamentSetting tS = new TournamentSetting(players, maxToken, score, judge);
        Tournament t = new Tournament(tS);

        BoardSetting bS = new BoardSetting(players, t.Inner, t.GameTokens, t.TokensPerPlayer, judge);
        Board b = new Board(bS);

        return new CustomTemplate(b, t);

    }
}

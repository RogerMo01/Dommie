using DominoLibrary;
using System.Diagnostics;
namespace Utils;

public static class Utils
{
    public static Dictionary<IPlayer, ConsoleColor> AssignColors(IPlayer[] players)
    {
        ConsoleColor[] colors = {
            ConsoleColor.Blue,
            ConsoleColor.Cyan,
            ConsoleColor.Magenta,
            ConsoleColor.Green,
            ConsoleColor.DarkBlue,
            ConsoleColor.Yellow
        };

        Dictionary<IPlayer, ConsoleColor> assignation = new();

        for (int i = 0; i < players.Length; i++)
        {
            assignation.Add(players[i], colors[i]);
        }

        return assignation;
    }

    public static void Lapse(int seconds)
    {
        Stopwatch crono = new Stopwatch();

        crono.Start();
        while (crono.ElapsedMilliseconds < seconds * 1000){}
        crono.Stop();
    }

    public static Token RandomTokenGenerator(List<Token> playersTokens)
    {
        Random r = new Random();
        Token selectionToken = playersTokens[r.Next(playersTokens.Count)];

        return selectionToken;
    }

    public static List<Token> GetAllTokens(Dictionary<IPlayer, List<Token>> playerTokens)
    {
        List<Token> allTokens = new();
        IPlayer[] players = playerTokens.Keys.ToArray();

        foreach (var player in players)
        {
            foreach (var token in playerTokens[player])
            {
                allTokens.Add(token);
            }
        }

        return allTokens;
    }

    public static List<Token> GenerateTokens(int maxToken)
    {
        List<Token> gameTokens = new List<Token>();

        for(int i = -1; i < maxToken; i++)
        {
            for (int j = i + 1; j <= maxToken; j++)
            {
                gameTokens.Add(new Token(i + 1, j));
            }
        }

        return gameTokens;    
    }

    public static int DecideTokensPerPlayer(int totalTokens, int totalPlayers)
    {
        return (totalTokens - ((totalTokens * 29) / 100)) / totalPlayers; 
    }

    public static Dictionary<Team, int> SetTeamsScore(List<Team> teams)
    {
        Dictionary<Team, int> scores = new();

        for (int i = 0; i < teams.Count; i++)
        {
            scores.Add(teams[i], 0);
        }

        return scores;
    }
}
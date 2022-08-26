using Utils;
namespace DominoLibrary;

public delegate int PointsGetter(GameStatus gameStatus, Team winner, Dictionary<IPlayer, List<Token>> playersTokens);

public class PointsWinner
{
    public static int ClassicGetPoints(GameStatus gameStatus, Team winner, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Dictionary<IPlayer, int> finalPuntuation = PresetPlayerPoints(playersTokens);

        int points = 0;
        if(winner == null) return 0;

        Node<IPlayer> currentPlayer = gameStatus.Players!.First;
        for (int i = 0; i < finalPuntuation.Count; i++)
        {
            if(!winner.Contains(currentPlayer.Value))
            {
                points += finalPuntuation[currentPlayer.Value];
            }
            currentPlayer = currentPlayer.Next!;
        }

        return points;
    }

    public static int Get5MultiplesPoints(GameStatus gameStatus, Team winner, Dictionary<IPlayer, List<Token>> playersTokens)
    {   
        Dictionary<IPlayer, int> finalPuntuation = PresetPlayerPoints(playersTokens);
        
        int points = 0;
        if(winner == null!) return points;

        Node<IPlayer> currentPlayer = gameStatus.Players!.First;
        for (int i = 0; i < finalPuntuation.Count; i++)
        {
            if(!winner.Contains(currentPlayer.Value))
            {
                points += 5 * playersTokens[currentPlayer.Value].Count;
            }
            currentPlayer = currentPlayer.Next!;   
        }

        return points;
    }

    private static Dictionary<IPlayer, int> PresetPlayerPoints(Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Dictionary<IPlayer, int> finalPuntuation = new Dictionary<IPlayer, int>();
        foreach (var item in playersTokens)
        {
            finalPuntuation.Add(item.Key, GetPlayerScore(item.Value));
        }

        return finalPuntuation;
    }

    private static int GetPlayerScore(List<Token> tokens)
    {
        int value = 0;
        foreach (var token in tokens)
        {
            value += token.Points;
        }

        return value;
    }
}
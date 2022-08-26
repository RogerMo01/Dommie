using Utils;
namespace DominoLibrary;

public delegate (Team, int) WinnerRoundGetter(GameStatus gameStatus, PointsGetter pointsGetter, Dictionary<IPlayer, List<Token>> playersTokens);

public static class RoundWinners
{
    public static (Team, int) ClassicGetWinner(GameStatus gameStatus, PointsGetter pointsGetter, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Team winner = gameStatus.Teams!.First();
        
        if(gameStatus.Teams!.Any(x => x.PlayersTeam.Any(x => playersTokens[x].Count == 0)))
        {
            winner = gameStatus.Teams!.First(x => x.PlayersTeam.Any(x => playersTokens[x].Count == 0));
        }
        else 
        {
            int pointsPerPlayer = int.MaxValue; 
            IPlayer currentPlayer = gameStatus.Players!.First();

            foreach (var team in gameStatus.Teams!)
            {
                foreach (var player in team.PlayersTeam)
                {
                    int  currentPointsPerPlayer= 0;
                    for (int i = 0; i < playersTokens[player].Count; i++)
                    {
                        currentPointsPerPlayer += playersTokens[player][i].Points;
                    }

                    if(currentPointsPerPlayer < pointsPerPlayer)
                    {
                        pointsPerPlayer = currentPointsPerPlayer;
                        currentPlayer = player;
                        winner = team;
                    }

                    if(( currentPointsPerPlayer == pointsPerPlayer) && (!team.Contains(currentPlayer)))
                    {
                        winner = null!;
                    }
                }
            }
        }

        return (winner, pointsGetter(gameStatus, winner, playersTokens));
    }

    public static (Team, int) Smallest5MultipleGetWinner(GameStatus gameStatus, PointsGetter pointsGetter, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Team winner = gameStatus.Teams!.First();

        int value = int.MaxValue;
        IPlayer currentPlayer = gameStatus.Players!.First();

        foreach (var team in gameStatus.Teams!)
        {
            foreach (var player in team.PlayersTeam)
            {
                int pointsPerPlayer = 0;
                for (int i = 0; i < playersTokens[player].Count; i++)
                {
                    pointsPerPlayer += playersTokens[player][i].Points;
                }

                if(((pointsPerPlayer % 5) == 0) && (pointsPerPlayer != 0))
                {
                    if(pointsPerPlayer < value)
                    {
                        value = pointsPerPlayer;
                        currentPlayer = player;
                        winner = team;
                    }

                    else if((pointsPerPlayer == value) && (!team.Contains(currentPlayer)))
                    {
                        winner = null!;
                    }
                }
            }
        }

        if(value == int.MaxValue) winner = null!;

        return (winner, pointsGetter(gameStatus, winner, playersTokens));
    }

    public static (Team, int) GetRandomWinner(GameStatus gameStatus, PointsGetter pointsGetter, Dictionary<IPlayer, List<Token>> playersToken)
    {
        Random random = new Random();
        Team winner = gameStatus.Teams![random.Next(gameStatus.Teams.Count)];

        return (winner, pointsGetter(gameStatus, winner, playersToken));
    }
    
}

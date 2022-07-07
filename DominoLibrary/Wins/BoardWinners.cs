using Utils;
namespace DominoLibrary;

public delegate (Team, int) WinnerBoard(Board board, PointsGetter pointsGetter, Dictionary<IPlayer, List<Token>> playersTokens);

public static class BoardWinners
{
    public static (Team, int) ClassicGetWinner(Board board, PointsGetter pointsGetter, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Team winner = board.Teams.First();
        
        if(board.Teams.Any(x => x.PlayersTeam.Any(x => playersTokens[x].Count == 0)))
        {
            winner = board.Teams.First(x => x.PlayersTeam.Any(x => playersTokens[x].Count == 0));
        }
        else 
        {
            int pointsPerPlayer = int.MaxValue; 
            IPlayer currentPlayer = board.Players.First();

            foreach (var team in board.Teams)
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

        return (winner, pointsGetter(board, winner, playersTokens));
    }

    public static (Team, int) Smallest5MultipleGetWinner(Board board, PointsGetter pointsGetter, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Team winner = board.Teams.First();

        int value = int.MaxValue;
        IPlayer currentPlayer = board.Players.First();

        foreach (var team in board.Teams)
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

                    if((pointsPerPlayer == value) && (!team.Contains(currentPlayer)))
                    {
                        winner = null!;
                    }
                }
            }
        }

        if(value == int.MaxValue) winner = null!;

        return (winner, pointsGetter(board, winner, playersTokens));
    }

    public static (Team, int) GetRandomWinner(Board board, PointsGetter pointsGetter, Dictionary<IPlayer, List<Token>> playersToken)
    {
        Random random = new Random();
        Team winner = board.Teams[random.Next(board.Teams.Count - 1)];

        return (winner, pointsGetter(board, winner, playersToken));
    }
    
}

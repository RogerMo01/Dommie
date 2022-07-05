using Utils;
namespace DominoLibrary;

public delegate (Team, int) WinnerBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens);

public static class BoardWinners
{
    public static (Team, int) ClassicGetWinner(Board board, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Team winner = board.Team.First();
        int points = 0;

        Dictionary<IPlayer, int> finalPuntuation = new Dictionary<IPlayer, int>();

        foreach (var item in playersTokens)
        {
            finalPuntuation.Add(item.Key, GetPlayerScore(item.Value));
        }

        if(board.Team.Any(x => x.PlayersTeam.Any(x => playersTokens[x].Count == 0)))
        {
            winner = board.Team.First(x => x.PlayersTeam.Any(x => playersTokens[x].Count == 0));
        }
        else 
        {
            int pointsPerPlayer = int.MaxValue; 
            IPlayer temporalPlayer = board.Players.First();

            foreach (var team in board.Team)
            {
                foreach (var player in team.PlayersTeam)
                {
                    if(finalPuntuation[player] < pointsPerPlayer)
                    {
                        pointsPerPlayer = finalPuntuation[player];
                        temporalPlayer = player;
                        winner = team;
                    }

                    if((finalPuntuation[player] == pointsPerPlayer) && (!team.Contains(temporalPlayer)))
                    {
                        winner = null!;
                    }
                }
            }
        }

        if(winner == null!) return (winner!, points);

        // sumar puntos
        Node<IPlayer> current = board.Players.First;
        for (int i = 0; i < finalPuntuation.Count; i++)
        {
            if(!winner.Contains(current.Value))
            {
                points += finalPuntuation[current.Value];
            }
            current = current.Next!;
        }

        return (winner, points);
    }

    public static (Team, int) Smallest5MultipleGetWinner(Board board, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Team winner = board.Team.First();
        int points = 0;

        int value = int.MaxValue;
        IPlayer currentPlayer = board.Players.First();

        foreach (var team in board.Team)
        {
            foreach (var player in team.PlayersTeam)
            {
                int aux = 0;
                for (int i = 0; i < playersTokens[player].Count; i++)
                {
                    aux += playersTokens[player][i].Points;
                }

                if(((aux % 5) == 0) && (aux != 0))
                {
                    if(aux < value)
                    {
                        value = aux;
                        currentPlayer = player;
                        winner = team;
                    }

                    if((aux == value) && (!team.Contains(currentPlayer)))
                    {
                        winner = null!;
                    }
                }
            }
        }

        if(value == int.MaxValue) winner = null!;
        if(winner == null) return (winner!, points);
      
        // sumar puntos
        Dictionary<IPlayer, int> finalPuntuation = new Dictionary<IPlayer, int>();

        foreach (var item in playersTokens)
        {
            finalPuntuation.Add(item.Key, GetPlayerScore(item.Value));
        }

        Node<IPlayer> current = board.Players.First;
        for (int i = 0; i < finalPuntuation.Count; i++)
        {
            if(!winner.Contains(current.Value))
            {
                points += 5 * finalPuntuation[current.Value];
            }
            current = current.Next!;   
        }

        return (winner, points); 
    }

    public static (Team, int) GetRandomWinner(Board board, Dictionary<IPlayer, List<Token>> playersToken)
    {
        Random random = new Random();
        Team winner = board.Team[random.Next(board.Team.Count - 1)];

        // buscar la mayor puntuacion de fichas en mesa
        int points = 0;
        Node<IPlayer> player = board.Players.First;
        for (int i = 0; i < playersToken.Count; i++)
        {
            int count = 0;
            foreach (var token in playersToken[player.Value])
            {
                count += token.Points;
            }

            if(count > points) points = count;
            player = player.Next!;
        }

        return (winner, points);
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

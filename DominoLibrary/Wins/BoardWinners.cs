using Utils;
namespace DominoLibrary;

public delegate (Team, int) WinnerBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens);

public static class BoardWinners
{
    public static (Team, int) ClassicGetWinner(Board board, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Team winner = board.Team.First();
    
        List<(IPlayer, int)> finalPuntuation = new List<(IPlayer, int)>();

        foreach (var item in playersTokens)
        {
            finalPuntuation.Add((item.Key, GetPlayerScore(item.Value)));
        }

        int value = int.MaxValue;

        foreach (var team in board.Team)
        {
            foreach (var player in team.PlayersTeam)
            {
                for (int i = 0; i < finalPuntuation.Count; i++)
                {
                    if(finalPuntuation[i].Item1 == player)
                    {
                        if(finalPuntuation[i].Item2 == 0)
                        {
                            winner = team;
                            value = 0;
                            break;
                        }   
                
                        else
                        {
                            if(finalPuntuation[i].Item2 == value)
                            {
                                winner = null!;
                                break;
                            }

                            if(finalPuntuation[i].Item2 < value)
                            {
                                winner = team;
                                value = finalPuntuation[i].Item2;
                                break;
                            }

                            if(finalPuntuation[i].Item2 > value) break;  
                        }
                    }
                }
            }
        }
        
        // sum victory points
        bool[] gameWinner = GameWinner(winner, board.Players.ToArray());
        
        int points = 0;
        foreach (var player in winner.PlayersTeam)
        {
            for (int i = 0; i < finalPuntuation.Count; i++)
            {
                if((player != finalPuntuation[i].Item1) && (gameWinner[i] == false))
                {
                    points += finalPuntuation[i].Item2;
                    gameWinner[i] = true;
                }
            }
        }

        return (winner, points);
    }

    private static int GetPlayerScore(List<Token> tokens)
    {
        int value = 0;

        foreach (var token in tokens)
        {
            value += (token.Left + token.Right);
        }

        return value;
    }


    public static (Team, int) FewerTokens(Board board, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Team winner = board.Team.First();
        int tokens = int.MaxValue;
        int points = 0;

        foreach (var team in board.Team)
        {
            foreach (var player in team.PlayersTeam)
            {
                if(playersTokens[player].Count < tokens)
                {
                    tokens = playersTokens[player].Count;
                    winner = team;
                }

                if((playersTokens[player].Count == tokens) && (!team.Contains(player)))
                {
                    winner = null!;
                }
            }   
        }

        if(winner == null) return (winner!, points);

        IPlayer[] players = board.Players.ToArray();
        bool[] gameWinner = GameWinner(winner, players);

        foreach (var player in winner.PlayersTeam)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if((players[i] != player) && (gameWinner[i] == false))
                {
                    points += 5 * playersTokens[players[i]].Count;
                    gameWinner[i] = true;
                }
            }
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
    private static bool[] GameWinner(Team winner, IPlayer[] players)
    {
        bool[] gameWinner = new bool[players.Length];
        for (int i = 0; i < winner.PlayersTeam.Count; i++)
        {
            for (int j = 0; j < players.Length; j++)
            {
               if(players[j] == winner.PlayersTeam[i]) 
               {
                    gameWinner[j] = true;
                    break;
               }
            }
        }
        return gameWinner;
    }
}

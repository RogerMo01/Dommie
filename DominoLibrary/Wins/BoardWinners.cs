using Utils;
namespace DominoLibrary;

public delegate (Team, int) WinnerBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens);

public static class BoardWinners
{
    public static (Team, int) ClassicGetWinner(Board board, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Team winner = board.Team.First();
        int points = 0;

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
        bool[] gameWinner = GameWinner(winner, finalPuntuation);
        
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

    private static bool[] GameWinner(Team winner, List<(IPlayer, int)> finalPuntuation)
    {
        bool[] gameWinner = new bool[finalPuntuation.Count];
        for (int i = 0; i < winner.PlayersTeam.Count; i++)
        {
            for (int j = 0; j < finalPuntuation.Count; j++)
            {
               if(finalPuntuation[j].Item1 == winner.PlayersTeam[i]) 
               {
                    gameWinner[j] = true;
                    break;
               }
            }
        }

        return gameWinner;
    }
}
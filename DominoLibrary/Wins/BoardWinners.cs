using Utils;
namespace DominoLibrary;

public delegate (IPlayer, int) WinnerBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens);

public static class BoardWinners
{
    public static (IPlayer, int) ClassicGetWinner(Board board, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        IPlayer winner = board.Players.First.Value;
        int points = 0;

        Dictionary<IPlayer, int> finalPuntuation = new Dictionary<IPlayer, int>();

        foreach (var item in playersTokens)
        {
            finalPuntuation.Add(item.Key, GetPlayerScore(item.Value));
        }

        if(finalPuntuation.Any(x => x.Value == 0))
        {
            winner = finalPuntuation.First(x => x.Value == 0).Key;
        }
        else
        {
            Node<IPlayer> player = board.Players.First;
            int value = int.MaxValue;

            for (int i = 0; i < board.Players.Count; i++)
            {
                if(finalPuntuation[player!.Value] == value)
                {
                    winner = null!;
                }

                if(finalPuntuation[player!.Value] < value)
                {
                    winner = player.Value;
                    value = finalPuntuation[player.Value];
                }

                player = player.Next!;
            }
        }

        // sum victory points
        foreach (var item in finalPuntuation)
        {
            if(item.Key != winner) points += item.Value;
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
}
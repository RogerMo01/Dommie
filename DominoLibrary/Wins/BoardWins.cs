using Utils;
namespace DominoLibrary;

public delegate bool WinBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens);

public static class BoardWins
{
    public static bool ClassicWinBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        if(board.ConsecutivePasses == 4) return true;

        Node<IPlayer> player = board.Players.First;

        for (int i = 0; i < board.Players.Count; i++)
        {
            if(playersTokens[player.Value].Count == 0) return true;
            player = player.Next!;
        }

        return false;
    }

    public static bool WinOther(Board board, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        throw new NotImplementedException();
    }
}
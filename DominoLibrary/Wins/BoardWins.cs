using Utils;
namespace DominoLibrary;

public delegate bool WinBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens, Token token);

public static class BoardWins
{
    public static bool ClassicWinBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens, Token token)
    {
        if(token.IsPass()) return false;

        if(board.ConsecutivePasses == board.Players.Count) return true;
        else
        {
            return PlayerWithoutToken(board, playersTokens);
        }
    }

    public static bool CrazyTokenWinBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens, Token token)
    {
        if(token.IsPass()) return false;

        // crazy token on board
        if((board.BoardTokens.First!.Value.EqualTokens(token)) || (board.BoardTokens.Last!.Value.EqualTokens(token))) return true;
        else
        {
            return ClassicWinBoard(board, playersTokens, token);
        } 
    }

    private static bool PlayerWithoutToken(Board board, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Node<IPlayer> player = board.Players.First;

        for (int i = 0; i < board.Players.Count; i++)
        {
            if(playersTokens[player.Value].Count == 0) return true;
            player = player.Next!;
        }

        return false;
    }
}
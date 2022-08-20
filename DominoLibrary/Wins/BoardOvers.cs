using Utils;
namespace DominoLibrary;

public delegate bool OverBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens, Token token);

public static class BoardOvers
{
    public static bool ClassicOverBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens, Token crazyToken)
    {
        if(AllPass(board.Plays, board.Players.Count)) return true;
        else
        {
            return PlayerWithoutToken(board, playersTokens);
        }
    }
    private static bool AllPass(List<(IPlayer player, IPlay play)> plays, int numberPlayers)
    {
        if(plays.Count < numberPlayers) return false;
        bool result = true;

        for (int i = plays.Count - 1; i >= plays.Count - numberPlayers; i--)
        {
            if(!(plays[i].play is Pass)) result = false;
        }
        return result;
    }

    public static bool CrazyTokenWinBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens, Token crazyToken)
    {
        // crazy token on board
        IPlay playedToken = board.Plays.Last().play;
        if(((playedToken.Left == crazyToken.Left && playedToken.Right == crazyToken.Right) || (playedToken.Left == crazyToken.Right && playedToken.Right == crazyToken.Left)) || 
        AllPass(board.Plays, board.Players.Count)) return true;
        {
            return false;
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
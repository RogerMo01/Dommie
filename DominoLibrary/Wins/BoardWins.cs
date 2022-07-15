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
    private static bool AllPass(List<(IPlayer player, Token_onBoard token_OnBoard)> plays, int numberPlayers)
    {
        if(plays.Count < numberPlayers) return false;
        bool result = true;

        for (int i = plays.Count - 1; i >= plays.Count - numberPlayers; i--)
        {
            if(!plays[i].token_OnBoard.IsPass()) result = false;
        }
        return result;
    }

    public static bool CrazyTokenWinBoard(Board board, Dictionary<IPlayer, List<Token>> playersTokens, Token crazyToken)
    {
        if(board.Plays.Last().token_OnBoard.Equals(crazyToken)) return true;

        return false;
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
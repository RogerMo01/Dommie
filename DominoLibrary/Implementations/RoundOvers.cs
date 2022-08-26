using Utils;
namespace DominoLibrary;

public delegate bool OverRound(GameStatus gameStatus, Dictionary<IPlayer, List<Token>> playersTokens, Token token);

public static class RoundOvers
{
    public static bool ClassicOverRound(GameStatus gameStatus, Dictionary<IPlayer, List<Token>> playersTokens, Token crazyToken)
    {
        if(AllPass(gameStatus.Plays!, gameStatus.Players!.Count)) return true;
        else
        {
            return PlayerWithoutToken(gameStatus, playersTokens);
        }
    }
    private static bool AllPass(List<IPlay> plays, int numberPlayers)
    {
        if(plays.Count < numberPlayers) return false;
        bool result = true;

        for (int i = plays.Count - 1; i >= plays.Count - numberPlayers; i--)
        {
            if(!(plays[i] is Pass)) result = false;
        }
        return result;
    }

    public static bool CrazyTokenOverRound(GameStatus gameStatus, Dictionary<IPlayer, List<Token>> playersTokens, Token crazyToken)
    {
        // crazy token on board
        IPlay playedToken = gameStatus.Plays!.Last();
        if(((playedToken.Left == crazyToken.Left && playedToken.Right == crazyToken.Right) || (playedToken.Left == crazyToken.Right && playedToken.Right == crazyToken.Left)) || 
        AllPass(gameStatus.Plays!, gameStatus.Players!.Count)) return true;
        {
            return false;
        }
    }

    private static bool PlayerWithoutToken(GameStatus gameStatus, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Node<IPlayer> player = gameStatus.Players!.First;

        for (int i = 0; i < gameStatus.Players.Count; i++)
        {
            if(playersTokens[player.Value].Count == 0) return true;
            player = player.Next!;
        }

        return false;
    }
}
namespace DominoLibrary;

public class Judge
{
    public OverBoard OverBoard {get; private set;}
    public WinnerBoard WinnerBoard {get; private set;}
    public PointsGetter WinnerPointsGetter {get; private set;}

    public Judge(OverBoard winB, WinnerBoard winnerB, PointsGetter pointsW)
    {
        OverBoard = winB;
        WinnerBoard = winnerB;
        WinnerPointsGetter = pointsW;
    }

    public bool IsValid(Board board, IPlay play)
    {
        if(play is Pass)
        {
            return false;
        }
        else
        {
            if(board.BoardTokens.Count == 0) return true;

            if(play.PlayRight && ((play.Straight && board.Ends[1] == play.Left) || (!play.Straight && board.Ends[1] == play.Right))) return true;

            if(!play.PlayRight && ((play.Straight && board.Ends[0] == play.Right) || (!play.Straight && board.Ends[0] == play.Left))) return true;
        }
        
        return false;
    }
}
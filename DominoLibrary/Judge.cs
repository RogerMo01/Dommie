namespace DominoLibrary;

public class Judge
{
    public OverRound OverBoard {get; private set;}
    public WinnerRoundGetter WinnerBoard {get; private set;}
    public PointsGetter WinnerPointsGetter {get; private set;}
    public InnerGetter InnerSelector { get; }
    public HandOut HandOutJudgment { get; }


    public Judge(OverRound winB, WinnerRoundGetter winnerB, PointsGetter pointsW, InnerGetter innerSelect, HandOut handOut)
    {
        OverBoard = winB;
        WinnerBoard = winnerB;
        WinnerPointsGetter = pointsW;
        InnerSelector = innerSelect;
        HandOutJudgment = handOut;
    }

    public bool IsValid(int boardTokensQtty, int[] ends, IPlay play)
    {
        if(play is Pass)
        {
            return false;
        }
        else
        {
            if(boardTokensQtty == 0) return true;

            if(play.PlayRight && ((play.Straight && ends![1] == play.Left) || (!play.Straight && ends![1] == play.Right))) return true;

            if(!play.PlayRight && ((play.Straight && ends![0] == play.Right) || (!play.Straight && ends![0] == play.Left))) return true;
        }
        
        return false;
    }
}
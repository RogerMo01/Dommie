namespace DominoLibrary;

public interface IJudge
{
    OverBoard WinBoard {get;}
    WinnerBoard WinnerBoard {get;}
    bool IsValid(Board board, Token_onBoard token);
}

public class Judge
{
    public OverBoard WinBoard {get; private set;}
    public WinnerBoard WinnerBoard {get; private set;}

    public Judge(OverBoard winB, WinnerBoard winnerB)
    {
        WinBoard = winB;
        WinnerBoard = winnerB;
    }

    public bool IsValid(Board board, Token_onBoard token)
    {
        if(token.IsPass())
        {
            return false;
        }
        else
        {
            if(board.BoardTokens.Count == 0) return true;

            if(token.PlayRight && ((token.Straight && board.Ends[1] == token.Left) || (!token.Straight && board.Ends[1] == token.Right))) return true;

            if(!token.PlayRight && ((token.Straight && board.Ends[0] == token.Right) || (!token.Straight && board.Ends[0] == token.Left))) return true;
        }
        return false;
    }

}
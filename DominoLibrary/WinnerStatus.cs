namespace DominoLibrary;

public class WinnerStatus
{
    public Team Winner { get; private set; }
    public int Score { get; private set; }

    public WinnerStatus(Team winner)
    {
        Winner = winner;
    }
    public WinnerStatus(Team winner, int score)
    {
        Winner = winner;
        Score = score;
    }
}
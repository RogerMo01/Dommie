namespace DominoLibrary;

public class GameResult
{
    public Team Winner { get; private set; }
    public int Score { get; private set; }

    public GameResult(Team winner)
    {
        Winner = winner;
    }
    public GameResult(Team winner, int score)
    {
        Winner = winner;
        Score = score;
    }
}
namespace DominoLibrary;

public class GameResult
{
    public IPlayer Winner { get; private set; }
    public int Score { get; private set; }

    public GameResult(IPlayer winner)
    {
        Winner = winner;
    }
    public GameResult(IPlayer winner, int score)
    {
        Winner = winner;
        Score = score;
    }
}
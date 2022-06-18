namespace DominoLibrary;

public class BoardResult
{
    public IPlayer Winner { get; private set; }

    public BoardResult(IPlayer winner)
    {
        Winner = winner;
    }
}
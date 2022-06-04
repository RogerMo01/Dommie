namespace DominoLibrary;

class GameResult
{
    public IPlayer Winner { get; private set; }

    public GameResult(IPlayer winner)
    {
        Winner = winner;
    }
}
namespace DominoLibrary;

public class Dustin : IPlayer
{
    public string Name { get; }

    public Dustin()
    {
        Name = "Dustin";
    }

    public Token_onBoard Play(BoardInfo info)
    {
        throw new NotImplementedException();
    }
}
namespace DominoLibrary;

public class Dustin : IPlayer
{
    public string Name { get; }

    public Dustin()
    {
        Name = "Dustin";
    }
    public Dustin(string newName)
    {
        Name = newName;
    }

    public Token_onBoard Play(PlayInfo info)
    {
        throw new NotImplementedException();
    }
}
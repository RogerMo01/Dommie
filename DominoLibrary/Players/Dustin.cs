namespace DominoLibrary;

public class Dustin : IPlayer
{
    public string Name { get; }
    public List<IStrategy> Strategies { get; }

    public Dustin(List<IStrategy> strategies)
    {
        Name = "Dustin";
        Strategies = strategies;
    }
    public Dustin(string newName, List<IStrategy> strategies)
    {
        Name = newName;
        Strategies = strategies;
    }

    public Token_onBoard Play(PlayInfo info)
    {
        return Strategies[0].Play(info, this);
    }
}
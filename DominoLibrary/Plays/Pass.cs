namespace DominoLibrary;

public class Pass : IPlay
{
    public int Left { get => -1;}
    public int Right { get => -1;}
    public int Points { get => -1; }
    public bool Straight { get => true; }
    public IPlayer Owner { get ; }
    public bool PlayRight { get => true; }

    public override string ToString() => "PASS";

    public IPlay Clone() => new Pass(Owner);

    public Pass(IPlayer owner)
    {
        Owner = owner;
    }
}
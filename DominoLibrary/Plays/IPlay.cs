namespace DominoLibrary;

public interface IPlay
{
    public int Left { get; }
    public int Right { get; }
    public int Points {get; }
    public bool Straight { get; }
    public IPlayer Owner { get; }
    public bool PlayRight { get; }
    public IPlay Clone();

    public string ToString();
}
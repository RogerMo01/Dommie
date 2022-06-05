namespace DominoLibrary;

public interface IPlayer
{
    public string Name { get; }
    Token Play(BoardInfo info);
}
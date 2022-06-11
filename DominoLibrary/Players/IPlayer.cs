namespace DominoLibrary;

public interface IPlayer
{
    public string Name { get; }
    Token_onBoard Play(BoardInfo info);
}
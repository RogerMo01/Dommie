namespace DominoLibrary;

public interface IPlayer
{
    public string Name { get; }
    public List<IStrategy> Strategies { get; }
    Token_onBoard Play(PlayInfo info);
}
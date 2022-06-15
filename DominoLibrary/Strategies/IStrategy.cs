namespace DominoLibrary;

public interface IStrategy
{
    Token_onBoard Play(PlayInfo info, IPlayer player);
}
namespace DominoLibrary;
using Utils;

public interface IGame
{
    IEnumerable<GameStatus> NextMove();
}

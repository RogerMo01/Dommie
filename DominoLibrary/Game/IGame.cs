namespace DominoLibrary;
using Utils;

public interface IGame 
{
    GameResult Start();

    void SetGamePrinter(GamePrinter gamePrinter);
}

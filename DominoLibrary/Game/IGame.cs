namespace DominoLibrary;
using Utils;

public interface IGame 
{
    GameResult RunRound();

    void SetGamePrinter(GamePrinter gamePrinter);
}

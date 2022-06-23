namespace DominoLibrary;
using Utils;

public interface IGame 
{
    public CircularList<IPlayer> Players {get; }
    public List<Token> GameTokens { get; }
    
    GameResult Start();

    void SetGamePrinter(GamePrinter gamePrinter);
}
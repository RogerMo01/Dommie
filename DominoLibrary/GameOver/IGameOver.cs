namespace DominoLibrary;

public interface IGameOver
{
    bool IsOver(LinkedList<Token> board);
}
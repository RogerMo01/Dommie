namespace DominoLibrary;

public interface IGameOver
{
    bool IsOver(LinkedList<Token_onBoard> board);
}
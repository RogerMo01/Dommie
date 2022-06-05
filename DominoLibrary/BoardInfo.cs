namespace DominoLibrary;

public class BoardInfo
{
    public List<Token> Tokens { get; private set; }
    public LinkedList<Token> BoardTokens { get; private set; }

    public BoardInfo(List<Token> tokens, LinkedList<Token> boardTokens)
    {
        Tokens = tokens;
        BoardTokens = boardTokens;
    }
}
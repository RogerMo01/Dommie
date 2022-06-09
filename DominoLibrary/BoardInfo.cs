namespace DominoLibrary;

public class BoardInfo
{
    public List<Token> Tokens { get; private set; }
    public LinkedList<Token_onBoard> BoardTokens { get; private set; }

    public BoardInfo(List<Token> tokens, LinkedList<Token_onBoard> boardTokens)
    {
        Tokens = tokens;
        BoardTokens = boardTokens;
    }
}
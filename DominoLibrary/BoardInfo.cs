namespace DominoLibrary;

public class PlayInfo
{
    public List<Token> Tokens { get; private set; }
    public LinkedList<Token_onBoard> BoardTokens { get; private set; }

    public PlayInfo(List<Token> tokens, LinkedList<Token_onBoard> boardTokens)
    {
        Tokens = tokens;
        BoardTokens = boardTokens;
    }
}
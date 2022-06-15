namespace DominoLibrary;

public class BotaGorda : IStrategy
{
    public Token_onBoard Play(Board board, List<Token> tokens, IPlayer player)
    {
        Token result = tokens[0];
        int[] ends = board.Ends;
        int points = -1;

        foreach (var token in tokens)
        {
            int value = 0;
            if(Playable(token, ends) || (board.BoardTokens.Count == 0))
            {
                value = token.Left + token.Right;
                if(value > points)
                {
                    points = value;
                    result = token;  
                }
            }
        }

        bool playRight = PlayRight(result, ends);
        bool straight = Straight(result, ends, playRight);

        return new Token_onBoard(result.Left, result.Right, straight, player, playRight);
    }

    private bool Playable(Token token, int[] ends)
    {

        foreach (var item in ends)
        {
            if(item == token.Left || item == token.Right) return true;
        }

        return false;
    }

    private bool PlayRight(Token token, int[] ends)
    {
        if(ends[1] == token.Left || ends[1] == token.Right) return true;
        
        return false;  
    }

    private bool Straight(Token token, int[] ends, bool playRight)
    {
        if(playRight && (ends[1] == token.Left)) { return true; }
        if(!playRight && (ends[0] == token.Right)) { return true; }

        return false; 
    } 
}
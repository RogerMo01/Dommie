namespace DominoLibrary;

public class BotaGorda : IStrategy
{
    public Token_onBoard Play(BoardInfo info, List<Token> tokens, IPlayer player)
    {
        Token current = tokens[0];
        int points = int.MinValue;

        foreach (var token in tokens)
        {
            int value = 0;
            if(Playable(token, info.Ends!) || (info.BoardTokens!.Count == 0))
            {
                value = token.Points;
                if(value > points)
                {
                    points = value;
                    current = token;  
                }
            }
        }
        
        bool playRight = true ? (info.Ends![1].Equals(current.Left) || info.Ends[1].Equals(current.Right) || info.BoardTokens!.Count == 0) : playRight = false;
        
        Token_onBoard result = new Token_onBoard(current, true, player, playRight);
        if(info.Judge!.IsValid(info.BoardTokens.Count, info.Ends, result)) return result;
        else return new Token_onBoard(current, false, player, playRight);
    }
    
    private bool Playable(Token token, int[] ends)
    {
        foreach (var item in ends)
        {
            if(item == token.Left || item == token.Right) return true;
        }
        
        return false;
    }

    public override string ToString() => "BotaGorda";
}
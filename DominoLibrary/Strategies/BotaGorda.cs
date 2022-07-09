namespace DominoLibrary;

public class BotaGorda : IStrategy
{
    public Token_onBoard Play(Board board, List<Token> tokens, IPlayer player)
    {
        Token current = tokens[0];
        int points = -1;

        foreach (var token in tokens)
        {
            int value = 0;
            if(Playable(token, board) || (board.BoardTokens.Count == 0))
            {
                value = token.Points;
                if(value > points)
                {
                    points = value;
                    current = token;  
                }
            }
        }
        
        bool playRight = true ? (board.Ends[1].Equals(current.Left) || board.Ends[1].Equals(current.Right) || board.BoardTokens.Count == 0) : playRight = false;
        
        Token_onBoard result = new Token_onBoard(current, true, player, playRight);
        if(board.Judge.IsValid(board, result)) return result;
        else return new Token_onBoard(current, false, player, playRight);
    }
    
    private bool Playable(Token token, Board board)
    {
        foreach (var item in board.Ends)
        {
            if(item == token.Left || item == token.Right) return true;
        }
        
        return false;
    }

    public override string ToString() => "BotaGorda";
    public string Info { get; } = " This Player always try to play the highest token score";
}
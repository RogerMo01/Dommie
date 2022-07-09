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
            if(Playable(token, board) || (board.BoardTokens.Count == 0))
            {
                value = token.Left + token.Right;
                if(value > points)
                {
                    points = value;
                    result = token;  
                }
            }
        }
        
        bool playRight = board.IsPlayableByRight(result);
        bool straight = board.Straight(result, playRight);

        return new Token_onBoard(result, straight, player, playRight);
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
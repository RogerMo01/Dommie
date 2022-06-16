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
            if(board.Playable(token) || (board.BoardTokens.Count == 0))
            {
                value = token.Left + token.Right;
                if(value > points)
                {
                    points = value;
                    result = token;  
                }
            }
        }

        bool playRight = board.PlayRight(result);
        bool straight = board.Straight(result, playRight);

        return new Token_onBoard(result.Left, result.Right, straight, player, playRight);
    }

}
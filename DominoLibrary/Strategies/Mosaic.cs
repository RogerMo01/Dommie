namespace DominoLibrary;

public class Mosaic : IStrategy
{
    public Token_onBoard Play(Board board, List<Token> tokens, IPlayer player)
    {
        Token result = tokens[0];

        int maxToken = tokens[tokens.Count - 1].Right;
       
        int[] total = new int[maxToken + 1];
        foreach (var token in tokens)
        {
            total[token.Left] ++;
            total[token.Right] ++;
        }

        int maxEnd = 0;
        int count = 0;

        if(board.BoardTokens.Count == 0)
        {
            maxEnd = total.Max();
        }

        else
        {
            if(total[board.Ends[0]] == total[board.Ends[1]]) maxEnd = Math.Max(board.Ends[0], board.Ends[1]);

            if(total[board.Ends[0]] > total[board.Ends[1]])
            {
                maxEnd = board.Ends[0];
            }
            else maxEnd = board.Ends[1];
        }

        GetToken(maxEnd, count, tokens, total, result);

        bool playRight = board.PlayRight(result);
        bool straight = board.Straight(result, playRight);

        return new Token_onBoard(result, straight, player, playRight);
    } 

    private void GetToken(int maxEnd, int count, List<Token> tokens, int[] total, Token result)
    {
        foreach (var token in tokens)
        {
            if((token.Left == maxEnd) || (token.Right == maxEnd))
            {
                if(token.Left == maxEnd)
                {
                    if(total[token.Right] > count)
                    {
                        count = total[token.Right];
                        result = token;
                    }
                }

                else
                {
                    if(total[token.Left] > count)
                    {
                        count = total[token.Left];
                        result = token;
                    }
                }
            }
        }
    } 
}
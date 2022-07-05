namespace DominoLibrary;

public class Mosaic : IStrategy
{
    public Token_onBoard Play(Board board, List<Token> tokens, IPlayer player)
    {
        Token result = tokens[0];
        
        int maxToken = board.GameTokens[board.GameTokens.Count - 1].Right;
       
        int[] total = new int[maxToken + 1];
        foreach (var token in tokens)
        {
            total[token.Left] ++;
            total[token.Right] ++;
        }

        int countToken = 0;
        int maxEnd = 0;

        if(board.BoardTokens.Count == 0)
        {
            for (int i = 0; i < total.Length; i++)
            {
                if(total[i] > countToken)
                {
                    countToken = total[i];
                    maxEnd = i;
                }
            }
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

        int count = 0;
        GetToken(ref maxEnd, ref count, tokens, total, ref result);

        bool playRight = board.PlayRight(result);
        bool straight = board.Straight(result, playRight);

        return new Token_onBoard(result, straight, player, playRight);
    } 

    private void GetToken(ref int maxEnd, ref int count, List<Token> tokens, int[] total, ref Token result)
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

    public override string ToString() => "Mosaic";
    public string Info { get; } = " This Player will always try to keep a mosaic of diferent tokens in hand";
}
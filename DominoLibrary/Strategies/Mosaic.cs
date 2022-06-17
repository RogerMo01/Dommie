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

        int max = 0;

        if(total[board.Ends[0]] == total[board.Ends[1]]) max = Math.Max(board.Ends[0], board.Ends[1]);
        
        if(total[board.Ends[0]] > total[board.Ends[1]])
        {
            max = board.Ends[0];
        }
        else max = board.Ends[1];
        

        int count = 0;
        foreach (var token in tokens)
        {
            if((token.Left == max) || (token.Right == max))
            {
                if(token.Left == max)
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

        bool playRight = board.PlayRight(result);
        bool straight = board.Straight(result, playRight);

        return new Token_onBoard(result, straight, player, playRight);
    }  
}
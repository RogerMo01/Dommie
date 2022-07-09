namespace DominoLibrary;

public class Mosaic : IStrategy
{
    public Token_onBoard Play(Board board, List<Token> tokens, IPlayer player)
    {
        int maxToken = board.GameTokens[board.GameTokens.Count - 1].Right;
       
        int[] countPerToken = new int[maxToken + 1];
        foreach (var token in tokens)
        {
            countPerToken[token.Left] ++;
            countPerToken[token.Right] ++;
        }

        int max = CurrentMaximum(board, tokens, countPerToken);
        
        List<Token> auxTokens = new List<Token>();
        foreach (var token in tokens)
        {
            if(max.Equals(token.Left) || max.Equals(token.Right))
            {
                auxTokens.Add(token);
            }
        }

        Token current = TokenMostAmountHand(auxTokens, countPerToken, max);
    
        bool playRight = true ? (board.Ends[1].Equals(current.Left) || board.Ends[1].Equals(current.Right) || board.BoardTokens.Count == 0) : playRight = false;

        Token_onBoard result = new Token_onBoard(current, true, player, playRight);
        if(board.Judge.IsValid(board, result)) return result;
        else return new Token_onBoard(current, false, player, playRight);
    }

    private Token TokenMostAmountHand(List<Token> tokens, int[] countPerToken, int max)
    {
        Token result = tokens.First();
        int aux = 0;

        foreach (var token in tokens)
        {
            int current = 0;
            if(token.Left != max)
            {
                current = countPerToken[token.Left];
            }

            else current = countPerToken[token.Right];

            if(current == aux)
            {
                if(result.Points < token.Points) result = token;
            }
            
            if(current > aux)
            { 
                aux = current;
                result = token;
            }
        }

        return result;
    }
 
    private int CurrentMaximum(Board board, List<Token> tokens, int[] countPerToken)
    {
        int max = 0;
        int currentMax = 0;

        if(board.BoardTokens.Count == 0)
        {
            for (int i = 0; i < countPerToken.Length; i++)
            {
                if(countPerToken[i] == currentMax)
                {
                    if(i > max) max = i;
                }

                if(countPerToken[i] > currentMax)
                {
                    currentMax = countPerToken[i];
                    max = i;
                }
            }
        }
        else
        {
            
            for (int i = 0; i < board.Ends.Length; i++)
            {
                if(countPerToken[board.Ends[i]] > currentMax)
                {
                    currentMax = countPerToken[board.Ends[i]];
                    max = board.Ends[i];
                }

                if(countPerToken[board.Ends[i]] == currentMax)
                {
                    if(board.Ends[i] > max) max = board.Ends[i];
                }
            }
        }

        return max;
    }

    
    public override string ToString() => "Mosaic";
    public string Info { get; } = " This Player will always try to keep a mosaic of diferent tokens in hand";
}
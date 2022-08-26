namespace DominoLibrary;

public class Mosaic : IStrategy
{
    public Token_onBoard Play(BoardInfo info, List<Token> tokens, IPlayer player)
    {
        int maxToken = info.MaxDouble;
       
        int[] countPerToken = new int[maxToken + 1];
        foreach (var token in tokens)
        {
            countPerToken[token.Left] ++;
            countPerToken[token.Right] ++;
        }

        int max = CurrentMaximum(info, tokens, countPerToken);
        
        List<Token> auxTokens = new List<Token>();
        foreach (var token in tokens)
        {
            if(max.Equals(token.Left) || max.Equals(token.Right))
            {
                auxTokens.Add(token);
            }
        }

        Token current = TokenMostAmountHand(auxTokens, countPerToken, max);
    
        bool playRight = true ? (info.Ends![1].Equals(current.Left) || info.Ends[1].Equals(current.Right) || info.BoardTokens!.Count == 0) : playRight = false;

        Token_onBoard result = new Token_onBoard(current, true, player, playRight);
        if(info.Judge!.IsValid(info.BoardTokens.Count, info.Ends, result)) return result;
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
 
    private int CurrentMaximum(BoardInfo info, List<Token> tokens, int[] countPerToken)
    {
        int max = 0;
        int currentMax = 0;

        if(info.BoardTokens!.Count == 0)
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
            
            for (int i = 0; i < info.Ends!.Length; i++)
            {
                if(countPerToken[info.Ends[i]] > currentMax)
                {
                    currentMax = countPerToken[info.Ends[i]];
                    max = info.Ends[i];
                }

                if(countPerToken[info.Ends[i]] == currentMax)
                {
                    if(info.Ends[i] > max) max = info.Ends[i];
                }
            }
        }

        return max;
    }

    
    public override string ToString() => "Mosaic";
}
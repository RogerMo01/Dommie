namespace DominoLibrary;

public class Random_Strategy : IStrategy
{
    public Token_onBoard Play(BoardInfo info, List<Token> tokens, IPlayer player)
    {
        Random random = new Random();
        if(info.BoardTokens!.Count == 0)
        { 
            return new Token_onBoard(tokens[random.Next(tokens.Count)], true, player, true);
        }
        else
        {
            List<Token> aux = Possible_TokensPlay(tokens, info.Ends!);
            Token current = aux[random.Next(aux.Count)];
        
            bool playRight = true ? (info.Ends![1].Equals(current.Left) || info.Ends[1].Equals(current.Right) || info.BoardTokens.Count == 0) : playRight = false;
            
            Token_onBoard result = new Token_onBoard(current, true, player, playRight);
            if(info.Judge!.IsValid(info.BoardTokens.Count, info.Ends, result)) return result;
            else return new Token_onBoard(current, false, player, playRight);
        }
    }

    private List<Token> Possible_TokensPlay(List<Token> tokens, int[] ends)
    {
        List<Token> result = new List<Token>();
        foreach (var token in tokens)
        {
            foreach (var item in ends)
            {
                if(item.Equals(token.Left) || item.Equals(token.Right))
                {
                    result.Add(token);
                    break;
                }
            }
        }

        return result;
    }

    public override string ToString() => "Random";
}

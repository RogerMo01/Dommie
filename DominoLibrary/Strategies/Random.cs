namespace DominoLibrary;

public class Random_Strategy : IStrategy
{
    public Token_onBoard Play(Board board, List<Token> tokens, IPlayer player)
    {
        Random random = new Random();
        if(board.BoardTokens.Count == 0)
        { 
            return new Token_onBoard(tokens[random.Next(tokens.Count - 1)], true, player, true);
        }
        else
        {
            List<Token> aux = Possible_TokensPlay(tokens, board.Ends);
            Token current = aux[random.Next(aux.Count - 1)];
        
            bool playRight = true ? (board.Ends[1].Equals(current.Left) || board.Ends[1].Equals(current.Right) || board.BoardTokens.Count == 0) : playRight = false;
            
            Token_onBoard result = new Token_onBoard(current, true, player, playRight);
            if(board.Judge.IsValid(board, result)) return result;
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
    public string Info { get; } = "This player will always play a random token among the valid tokens in his hand.";
}

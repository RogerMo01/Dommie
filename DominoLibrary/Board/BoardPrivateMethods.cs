namespace DominoLibrary;
using Utils;

public partial class Board
{
    private bool HaveToken(IPlayer player, bool firstPlay)
    {
        //si es la primera jugada siempre lleva
        if(firstPlay) return true;        

        //int[] ends = GetEnds(BoardTokens);
        List<Token> playerTokens = PlayersTokens[player];

        for (int i = 0; i < Ends.Length; i++)
        {
            for (int j = 0; j < playerTokens.Count; j++)
            {
                if(playerTokens[j].Right == Ends[i] || playerTokens[j].Left == Ends[i])
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    private void UpdateBoard(Token_onBoard token, IPlayer player)
    {
        try
        {
            if(token.PlayRight)
            {
                BoardTokens.AddLast(token);
            }
            else
            {
                BoardTokens.AddFirst(token);
            }

            int index = 0;

            for (int i = 0; i < PlayersTokens[player].Count; i++)
            {
                if((token.Left == PlayersTokens[player][i].Left) && (token.Right == PlayersTokens[player][i].Right))
                {
                    index = i;
                    break; 
                }
            }
            
            PlayersTokens[player].RemoveAt(index);

            ResetEnds();
        }
        catch (NullReferenceException){}

        LastPlayed = token;
        LastPlayer = player;
    }

    

    private static Dictionary<IPlayer, List<Token>> HandOut(List<Token> tokens, CircularList<IPlayer> players, int tokensPerPlayer)
    {
        Dictionary<IPlayer, List<Token>> result = new Dictionary<IPlayer, List<Token>>();

        List<Token> clonedTokens = tokens.ToList();

        Node<IPlayer> firstPlayer = players.First;

        for (int i = 0; i < players.Count; i++)
        {
            result.Add(firstPlayer.Value, HandOutToPlayer(clonedTokens, tokensPerPlayer));
            firstPlayer = firstPlayer.Next!;
        }

        return result;
    }

    private static List<Token> HandOutToPlayer(List<Token> tokens, int tokensPerPlayer)
    {
        List<Token> playerTokens = new List<Token>();
        Random random = new Random();

        for (int i = 0; i < tokensPerPlayer ; i++)  
        {
            int index = random.Next(tokens.Count - 1);
            playerTokens.Add(tokens[index]);
            tokens.RemoveAt(index);
        }

        return playerTokens;
    }
}
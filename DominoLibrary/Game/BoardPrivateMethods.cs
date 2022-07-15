namespace DominoLibrary;
using Utils;

public partial class Board
{
    private bool HaveToken(IPlayer player)
    {    
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
        if(!token.IsPass())
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

            // find token in player hand to remove it
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
    }

    private Token GetCrazyToken()
    {
        int handOut = Settings.TokensPerPlayer;

        Random random = new Random();
        int index = random.Next((handOut * Players.Count));

        int indexPlayer = index / handOut;
        int indexToken = index % handOut;

        return PlayersTokens[PlayersTokens.ToArray()[indexPlayer].Key][indexToken];
    }
}
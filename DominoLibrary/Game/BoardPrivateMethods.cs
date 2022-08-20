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

    private void UpdateBoard(IPlay play, IPlayer player)
    {
        if(!(play is Pass))
        {
            if(play.PlayRight)
            {
                BoardTokens.AddLast((Token_onBoard)play);
            }
            else
            {
                BoardTokens.AddFirst((Token_onBoard)play);
            }

            int index = 0;

            // find token in player hand to remove it
            for (int i = 0; i < PlayersTokens[player].Count; i++)
            {
                if((play.Left == PlayersTokens[player][i].Left) && (play.Right == PlayersTokens[player][i].Right))
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
        Random r = new Random();

        Token selectionToken = GameTokens.Last();

        do
        {
            selectionToken = GameTokens[r.Next(GameTokens.Count)];

        } while (!PlayersTokens.Any(x => x.Value.Contains(selectionToken)));

        return selectionToken;
    }
}
namespace DominoLibrary;
using Utils;

public delegate Dictionary<IPlayer, List<Token>> HandOut(List<Token> tokens, CircularList<IPlayer> players, int tokensPerPlayer);

public static class HandOuts
{
    public static Dictionary<IPlayer, List<Token>> Random_HandOut(List<Token> tokens, CircularList<IPlayer> players, int tokensPerPlayer)
    {
        Dictionary<IPlayer, List<Token>> result = new Dictionary<IPlayer, List<Token>>();

        List<Token> clonedTokens = tokens.ToList();

        Node<IPlayer> firstPlayer = players.First;

        for (int i = 0; i < players.Count; i++)
        {
            result.Add(firstPlayer.Value, HandOutToOnePlayer(clonedTokens, tokensPerPlayer));
            firstPlayer = firstPlayer.Next!;
        }

        return result;
    }

    private static List<Token> HandOutToOnePlayer(List<Token> tokens, int tokensPerPlayer)
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

    public static Dictionary<IPlayer, List<Token>> BiggerFirst(List<Token> tokens, CircularList<IPlayer> players, int tokensPerPlayer)
    {
        Dictionary<IPlayer, List<Token>> result = new Dictionary<IPlayer, List<Token>>();

        List<Token> clonedTokens = tokens.ToList();

        clonedTokens.Sort();

        IPlayer[] player = players.ToArray();
        Random random = new Random();
        int totalTokens = tokensPerPlayer * player.Length;

        while(totalTokens != 0)
        {
            IPlayer currentPlayer = player[random.Next(player.Length)];
    
            int index = clonedTokens.Count - 1;

            if(!result.ContainsKey(currentPlayer))
            {
                result.Add(currentPlayer, new List<Token>(){clonedTokens[index]});
            }
            else
            {
                result[currentPlayer].Add(clonedTokens[index]);
            }

            clonedTokens.RemoveAt(index);
            totalTokens--;
        }

        return result;
    }
}
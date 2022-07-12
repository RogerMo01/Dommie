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

        Node<IPlayer> currentPlayer = players.First;

        while(tokensPerPlayer != 0)
        {
            for(int i = 0; i < players.Count; i++)
            {
                int index = clonedTokens.Count - 1;

                if(!result.ContainsKey(currentPlayer.Value))
                {
                    result.Add(currentPlayer.Value, new List<Token>(){clonedTokens[index]});
                }
                else
                {
                    result[currentPlayer.Value].Add(clonedTokens[index]);
                }

                clonedTokens.RemoveAt(index);
                currentPlayer = currentPlayer.Next!;
            }

            tokensPerPlayer--;
        }

        return result;
    }
}
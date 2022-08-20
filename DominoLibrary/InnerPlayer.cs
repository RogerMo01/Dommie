namespace DominoLibrary;

public delegate IPlayer Inner(Dictionary<IPlayer, List<Token>> PlayersTokens);

public static class InnerPlayer
{
    public static IPlayer Random_Inner(Dictionary<IPlayer, List<Token>> PlayersTokens)
    {
        IPlayer[] players = PlayersTokens.Keys.ToArray();
        IPlayer inner = players[0];
        
        Random random = new Random();

        return players[random.Next(players.Length)];
    }

    public static IPlayer Bigger_Token(Dictionary<IPlayer, List<Token>> PlayersTokens)
    {
        IPlayer[] players = PlayersTokens.Keys.ToArray();
        IPlayer inner = players[0];

        Token currentBigger = PlayersTokens[inner].First();

        foreach (var item in players)
        {
            List<Token> tokens = PlayersTokens[item];
            tokens.Sort();

            if(tokens[tokens.Count - 1].Points > currentBigger.Points)
            {
                currentBigger = tokens[tokens.Count - 1];
                inner = item;
            }
        }

        return inner;
    }

    public static IPlayer Min_Double(Dictionary<IPlayer, List<Token>> PlayersTokens)
    {
        IPlayer[] players = PlayersTokens.Keys.ToArray();
        IPlayer inner = players[0];
        
        Token token = PlayersTokens[inner].First();

        bool isDouble = false;

        foreach (var player in players)
        {
            foreach (var item in PlayersTokens[player])
            {
                if((item.Left.Equals(item.Right)) && (item.Points <= token.Points))
                {
                    token = item; 
                    inner = player;
                    isDouble = true;
                }
            }
        }

        if(!isDouble) return Random_Inner(PlayersTokens);

        return inner;
    }

    
    public static IPlayer Max_Data(Dictionary<IPlayer, List<Token>> PlayersTokens)
    {
        int maxNumberToken = Max_NumberToken(PlayersTokens);
        IPlayer[] players = PlayersTokens.Keys.ToArray();
        List<(IPlayer, int[])> playersCountTokens = PlayersCountTokens(PlayersTokens, players, maxNumberToken);
        
        int aux = 0;
        IPlayer result = players[0];

        for (int i = 0; i < players.Length; i++)
        {
            int max = playersCountTokens[i].Item2.Max();

            if(max > aux) 
            {
                aux = max;
                result = players[i];
            }
        }

        return result;
    }

    private static int Max_NumberToken(Dictionary<IPlayer, List<Token>> PlayersTokens)
    {
        int result = 0;
        IPlayer[] players = PlayersTokens.Keys.ToArray();

        foreach (var player in players)
        {
            foreach (var token in PlayersTokens[player])
            {
                int aux = Math.Max(token.Left, token.Right);
                if(aux > result) result = aux;
            }
        }
        
        return result;
    }

    private static List<(IPlayer, int[])> PlayersCountTokens(Dictionary<IPlayer, List<Token>> PlayersTokens, IPlayer[] players, int maxNumberToken)
    {
        List<(IPlayer, int[])> playersCountTokens = new List<(IPlayer, int[])>();

        foreach (var player in players)
        {
            int[] numbers = new int[maxNumberToken + 1];

            foreach (var token in PlayersTokens[player])
            {
                numbers[token.Left] ++;
                numbers[token.Right] ++;
            }

            playersCountTokens.Add((player, numbers));
        }

        return playersCountTokens;
    }
}
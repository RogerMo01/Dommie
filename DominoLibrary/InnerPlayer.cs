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
}
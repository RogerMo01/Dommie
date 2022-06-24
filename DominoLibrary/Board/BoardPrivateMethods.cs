namespace DominoLibrary;
using Utils;

public partial class Board
{
    private bool IsOver()
    {
        if(ConsecutivePasses == 4) return true;

        Node<IPlayer> player = Players.First;

        for (int i = 0; i < Players.Count; i++)
        {
            if(PlayersTokens[player.Value].Count == 0) return true;
            player = player.Next!;
        }

        return false;
    }

    private (IPlayer, int) GetWinner()
    {
        IPlayer winner = Players.First.Value;
        int points = 0;

        Dictionary<IPlayer, int> finalPuntuation = new Dictionary<IPlayer, int>();

        foreach (var item in PlayersTokens)
        {
            finalPuntuation.Add(item.Key, GetPlayerScore(item.Value));
        }

        Node<IPlayer> player = Players.First;
        int value = int.MaxValue;
        if(ConsecutivePasses == Players.Count) // game over por tranque
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if(finalPuntuation[player!.Value] == value)
                {
                    winner = null!;
                }

                if(finalPuntuation[player!.Value] < value)
                {
                    winner = player.Value;
                    value = finalPuntuation[player.Value];
                }

                player = player.Next!;
            }
        }
        else
        {
            winner = finalPuntuation.First(x => x.Value == 0).Key;
        }

        // sumar puntos de victoria
        foreach (var item in finalPuntuation)
        {
            if(item.Key != winner) points += item.Value;
        }

        return (winner, points);
    }

    private static int GetPlayerScore(List<Token> tokens)
    {
        int value = 0;

        foreach (var token in tokens)
        {
            value += (token.Left + token.Right);
        }

        return value;
    }

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
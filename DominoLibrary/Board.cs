﻿using Utils;
namespace DominoLibrary;

public class Board
{
    CircularList<IPlayer> Players;
    List<Token> GameTokens;
    Dictionary<IPlayer, List<Token>> PlayersTokens;
    LinkedList<Token_onBoard> BoardTokens;
    Setting Settings;

    int ConsecutivePasses;

    public Board(Setting setting)
    {
        Settings = setting;

        Players = setting.Players;
        GameTokens = GenerateTokens(setting.MaxToken);

        int tokensPerPlayer = setting.MaxToken + 1;
        PlayersTokens = HandOut(GameTokens, Players, tokensPerPlayer);

        BoardTokens = new LinkedList<Token_onBoard>();   

        ConsecutivePasses = 0;
    }

    public GameResult Start()
    {
        Node<IPlayer> currentPlayer = Settings.Inner.Previous!;
    
        while (true)
        {
            currentPlayer = currentPlayer.Next!;

            if(HaveToken(currentPlayer.Value, BoardTokens.Count == 0))
            {
                BoardInfo info = new BoardInfo(PlayersTokens[currentPlayer.Value], BoardTokens);
                Token_onBoard token = currentPlayer.Value.Play(info);

                UpdateBoard(token, currentPlayer.Value);
                ConsecutivePasses = 0;
            }
            else
            {
                ConsecutivePasses ++;
            }

            // if(Settings.OverCondition.IsOver(BoardTokens)) { break; }

            if(IsOver()) {break; }
        }

        IPlayer winner = GetWinner();
        return new GameResult(winner);
    }

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

    private IPlayer GetWinner()
    {
        //valora quien ganó mirando el tablero
        IPlayer winner;
        int points = int.MaxValue;
        Node<IPlayer> player = Players.First;

        if(ConsecutivePasses == 4)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                int value = 0;
                for (int j = 0; j < PlayersTokens[player.Value].Count; j++)
                {
                    value += PlayersTokens[player.Value][j].Left + PlayersTokens[player.Value][j].Right;
                }

                if(value < points) 
                {
                    points = value;
                    winner = player.Value;
                }

                if(value == points)
                {
                    winner = player.Value;
                }

                player = player.Next!;
            }
        }
        else
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if(PlayersTokens[player.Value].Count == 0) winner = player.Value;
                player = player.Next!;
            }
        }

        return winner;
    }

    private bool HaveToken(IPlayer player, bool firstPlay)
    {
        //si es la primera jugada siempre lleva
        if(firstPlay) return true;        

        int[] ends = GetEnds(BoardTokens);
        List<Token> playerTokens = PlayersTokens[player];

        for (int i = 0; i < ends.Length; i++)
        {
            for (int j = 0; j < playerTokens.Count; j++)
            {
                if(playerTokens[j].Right == ends[i] || playerTokens[j].Left == ends[i])
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    private void UpdateBoard(Token_onBoard token, IPlayer player)
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
            if(PlayersTokens[player].Contains(token))
            {
                index = i;
                break;
            }   
        }
        
        PlayersTokens[player].RemoveAt(index);
    }

    private static List<Token> GenerateTokens(int maxToken)
    {
        List<Token> gameTokens = new List<Token>();

        for(int i = -1; i < maxToken; i++)
        {
            for (int j = i + 1; j <= maxToken; j++)
            {
                gameTokens.Add(new Token(i + 1, j));
            }
        }

        return gameTokens;    
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
    
    private static int[] GetEnds(LinkedList<Token_onBoard> boardTokens)
    {
        int[] ends = new int[2];
        Token_onBoard token = boardTokens.First.Value; // tener en cuenta que al inicio de la partida la lista siempre sera null

        ends[0] = (token.Straight) ? token.Left : token.Right;
    
        token = boardTokens.Last.Value;

        ends[1] = (token.Straight) ? token.Right : token.Left;

        return ends; 
    }
    
}
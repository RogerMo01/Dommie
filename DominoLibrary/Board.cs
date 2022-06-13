using Utils;
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
    
        //temporal showInConsole ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        System.Console.WriteLine();
        System.Console.WriteLine("Tokens hand out was:");

        ShowPlayerTokens(PlayersTokens);
        //

        while (true)
        {
            currentPlayer = currentPlayer.Next!;

            if(HaveToken(currentPlayer.Value, BoardTokens.Count == 0))
            {
                PlayInfo info = new PlayInfo(PlayersTokens[currentPlayer.Value], BoardTokens);
                Token_onBoard token = currentPlayer.Value.Play(info);

                //temporal showInConsole ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                string side = (token.PlayRight) ? "Right" : "Left";
                Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.WriteLine($"{currentPlayer.Value.Name} played [{token.Left}:{token.Right}] by {side}");
                Console.ForegroundColor = ConsoleColor.White;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                UpdateBoard(token, currentPlayer.Value);
                ConsecutivePasses = 0;
            }
            else
            {
                ConsecutivePasses ++;

                //temporal showInConsole ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"{currentPlayer.Value.Name} pass");
                Console.ForegroundColor = ConsoleColor.White;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            }

            if(IsOver()) {break; }
        }

        IPlayer winner = GetWinner();

        //temporal showInConsole ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        System.Console.WriteLine();
        System.Console.WriteLine("GAME OVER");
        Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine($"{winner.Name} is the WINNER");
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        return new GameResult(winner);
    }

    //temp method showInConsole ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private static void ShowPlayerTokens(Dictionary<IPlayer, List<Token>> playerTokens)
    {
        ConsoleColor[] colors = {ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.Cyan};
        int color = 0;
        
        foreach (var item in playerTokens)
        {
            Console.ForegroundColor = colors[color];
            System.Console.WriteLine($"{item.Key.Name}:");

            foreach (var token in item.Value)
            {
                System.Console.Write($"[{token.Left}:{token.Right}] ");
            }

            System.Console.WriteLine();
            color++;
        }
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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
        IPlayer winner = Players.First.Value;
        int points = int.MaxValue;
        Node<IPlayer> player = Players.First;

        if(ConsecutivePasses == 4)
        {
            foreach (var item in PlayersTokens)
            {
                int value = 0;

                foreach (var token in item.Value)
                {
                    value += (token.Left + token.Right);
                }

                if(value < points) 
                {
                    points = value;
                    winner = player.Value;
                }

                if(value == points)
                {
                    winner = null!;
                }

                player = player.Next!;
            }
        }

        else
        {
            foreach (var item in PlayersTokens)
            {
                if(item.Value.Count == 0) return item.Key;
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
    
    public static int[] GetEnds(LinkedList<Token_onBoard> boardTokens)
    {
        if(boardTokens.Count == 0) return new int [] {-1, -1}; // this line should never be reached
        
        int[] ends = new int[2];
        Token_onBoard token = boardTokens.First!.Value; // tener en cuenta que al inicio de la partida la lista siempre sera null

        ends[0] = (token.Straight) ? token.Left : token.Right;
    
        token = boardTokens.Last!.Value;

        ends[1] = (token.Straight) ? token.Right : token.Left;

        return ends; 
    }
    
}
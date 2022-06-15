using Utils;
namespace DominoLibrary;

public partial class Board
{
    public CircularList<IPlayer> Players {get; private set;}
    public List<Token> GameTokens {get; private set;}
    Dictionary<IPlayer, List<Token>> PlayersTokens;
    public LinkedList<Token_onBoard> BoardTokens {get; private set;}
    public int[] Ends { get; private set;} = {-1, -1};
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
                Token_onBoard token = currentPlayer.Value.Play(this, PlayersTokens[currentPlayer.Value]);

                //temporal showInConsole ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                string side = (token.PlayRight) ? "Right" : "Left";
                Console.ForegroundColor = ConsoleColor.Blue;

                if(token.Straight)
                {
                    System.Console.WriteLine($"{currentPlayer.Value.Name} played [{token.Left}:{token.Right}] by {side}");
                }
                else
                {
                    System.Console.WriteLine($"{currentPlayer.Value.Name} played [{token.Right}:{token.Left}] by {side}");
                }
                
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
    
    public void ResetEnds()
    {       
        Token_onBoard token = BoardTokens.First!.Value; // tener en cuenta que al inicio de la partida la lista siempre sera null

        Ends[0] = (token.Straight) ? token.Left : token.Right;
    
        token = BoardTokens.Last!.Value;

        Ends[1] = (token.Straight) ? token.Right : token.Left;
    }

    public bool Playable(Token token)
    {
        foreach (var item in Ends)
        {
            if(item == token.Left || item == token.Right) return true;
        }

        return false;
    }

    public bool PlayRight(Token token)
    {
        if(Ends[1] == token.Left || Ends[1] == token.Right) return true;
        
        return false;
    }

    public bool Straight(Token token, bool playRight)
    {
        if(playRight && (Ends[1] == token.Left)) { return true; }
        if(!playRight && (Ends[0] == token.Right)) { return true; }

        return false; 
    }

}
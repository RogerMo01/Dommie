namespace DominoLibrary;

public partial class GamePrinter
{
    Board? Board;
    Dictionary<IPlayer, List<Token>>? PlayerTokens;
    Tournament? Tournament;


    public GameResult PrintBoard()
    {
        ShowPlayerTokens();

        GameResult result = Board!.Start();

        PrintBoardWinner(result.Winner);

        return result;
    }

    public void AddBoard(Board board, Dictionary<IPlayer, List<Token>> playerTokens)
    {
        Board = board;
        PlayerTokens = playerTokens;
    }

    public void PrintPlay()
    {
        Token_onBoard play = Board!.LastPlayed!;
        IPlayer player = Board.LastPlayer!;
        
        Console.WriteLine();
        try // si no es null imprime jugada
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string side = (play.PlayRight) ? "Right" : "Left";

            Console.WriteLine( (play.Straight) ?  $"{play.Owner.Name} played [{play.Left}:{play.Right}] by {side}" : $"{play.Owner.Name} played [{play.Right}:{play.Left}] by {side}" );
            Console.ForegroundColor = ConsoleColor.White;
        }
        catch (NullReferenceException) // si es null es que no lleva
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{player.Name} pass");
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    private void ShowPlayerTokens()
    {
        ConsoleColor[] colors = {ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.Cyan};
        int color = 0;

        Console.WriteLine();
        Console.WriteLine("Tokens hand out was:");
        
        foreach (var item in PlayerTokens!)
        {
            Console.ForegroundColor = colors[color];
            Console.WriteLine($"{item.Key.Name}:");

            foreach (var token in item.Value)
            {
                Console.Write($"[{token.Left}:{token.Right}] ");
            }

            Console.WriteLine();
            color++;
        }
        Console.WriteLine();
    }

    private void PrintBoardWinner(IPlayer winner)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("ROUND OVER");

        Console.ForegroundColor = ConsoleColor.Yellow;
        
        try
        {
            Console.WriteLine($"\n{winner.Name} win this round");
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("\nTie Game");
        }
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
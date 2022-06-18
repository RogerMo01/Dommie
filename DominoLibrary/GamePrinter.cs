namespace DominoLibrary;

public class GamePrinter
{
    Board? Board;
    Dictionary<IPlayer, List<Token>>? PlayerTokens;

    
    public void PrintBoard()
    {
        ShowPlayerTokens();

        BoardResult result = Board!.Start();

        PrintWinner(result.Winner);        
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
        
        try // si no es null imprime jugada
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string side = (play.PlayRight) ? "Right" : "Left";

            System.Console.WriteLine( (play.Straight) ?  $"{play.Owner.Name} played [{play.Left}:{play.Right}] by {side}" : $"{play.Owner.Name} played [{play.Right}:{play.Left}] by {side}" );
            Console.ForegroundColor = ConsoleColor.White;
        }
        catch (NullReferenceException) // si es null es que no lleva
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"{player.Name} pass");
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    private void ShowPlayerTokens()
    {
        ConsoleColor[] colors = {ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.Cyan};
        int color = 0;

        System.Console.WriteLine();
        System.Console.WriteLine("Tokens hand out was:");
        
        foreach (var item in PlayerTokens!)
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

    private void PrintWinner(IPlayer winner)
    {
        System.Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        System.Console.WriteLine("GAME OVER");

        Console.ForegroundColor = ConsoleColor.Yellow;
        
        if(winner.Equals(null))
        {
            System.Console.WriteLine("Tie Game");
        }
        else
        {
            System.Console.WriteLine($"{winner.Name} is the WINNER");
        }
        Console.ForegroundColor = ConsoleColor.White;
        System.Console.WriteLine();
    }
}
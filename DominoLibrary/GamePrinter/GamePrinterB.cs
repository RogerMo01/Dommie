namespace DominoLibrary;

public partial class GamePrinter
{
    Board? Board;
    Dictionary<IPlayer, List<Token>>? PlayerTokens;
    Tournament? Tournament;

    public void AddBoard(Board board, Dictionary<IPlayer, List<Token>> playerTokens)
    {
        Board = board;
        PlayerTokens = playerTokens;
    }

    public void PrintPlay()
    {
        int size = Board!.Plays.Count;
        Token_onBoard play = Board.Plays[size - 1].token_OnBoard;
        IPlayer player = Board.Plays[size - 1].player;
        
        Console.WriteLine();
        if(play.IsPass())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{player.Name} pass");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string side = (play.PlayRight) ? "Right" : "Left";

            Console.WriteLine( (play.Straight) ?  $"{play.Owner.Name} played [{play.Left}:{play.Right}] by {side}" : $"{play.Owner.Name} played [{play.Right}:{play.Left}] by {side}" );
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void ShowPlayerTokens()
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

    public void PrintBoardWinner(IPlayer winner)
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
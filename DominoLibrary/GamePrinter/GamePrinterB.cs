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

    public void PrintPoints(int score)
    {
        System.Console.WriteLine($" {score} points scored");
    }

    public void PrintPlay()
    {
        if(Board!.Players.ToArray().Any(x => x is HumanPlayer))
        {
            Console.Clear();
        }

        int size = Board!.Plays.Count;
        IPlay play = Board.Plays[size - 1].play;
        IPlayer player = Board.Plays[size - 1].player;
        
        Console.WriteLine();
        if(play is Pass)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\n{player.Name} pass");
        }
        else
        {
            string side = (play.PlayRight) ? "Right" : "Left";

            Console.ForegroundColor = play.Owner.Color;
            Console.Write($"\n{play.Owner}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" played ");
            Console.ForegroundColor = play.Owner.Color;
            Console.Write(play);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" by {side}");

            Console.ForegroundColor = ConsoleColor.White;
        }
        
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void ShowPlayerTokens()
    {
        Console.WriteLine();
        Console.WriteLine("Tokens hand out was:");
        
        foreach (var item in PlayerTokens!)
        {
            Console.ForegroundColor = item.Key.Color;
            Console.WriteLine($"{item.Key.Name}:");

            foreach (var token in item.Value)
            {
                Console.Write($"[{token.Left}:{token.Right}] ");
            }

            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public void PrintBoardWinner(Team winner, int score)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("==== ROUND OVER ====");

        Console.ForegroundColor = ConsoleColor.Yellow;
        
        if(winner == null) 
        {
            Console.WriteLine("\nTie Game");
        }
        else
        {
            Console.WriteLine($"{winner} Win this round with {score} points");
        }
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
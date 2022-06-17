namespace DominoLibrary;

class GamePrinter
{
    Board Board;

    public GamePrinter(Board board)
    {
        Board = board;
    }

    public void PrintGame()
    {
        GameResult result = Board.Start();

        PrintWinner(result.Winner);        
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
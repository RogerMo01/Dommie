namespace DominoLibrary;

public partial class GamePrinter
{
    public GameResult PrintTournament()
    {
        GameResult result = Tournament!.Start();

        PrintTournamentWinner(result);

        return result;
    }

    public void AddTournament(Tournament tournament)
    {
        Tournament = tournament;
    }

    public void ShowTournamentStatus(int round, Dictionary<IPlayer, int> scores)
    {
        Console.Clear();
        Console.WriteLine("\nScore status");

        foreach (var item in scores)
        {
            System.Console.Write($"{item.Key}: {item.Value} points   |   ");
        }
        Console.WriteLine();
        
        Console.WriteLine($"\nRound #{round} is about to begin");
    }

    private void PrintTournamentWinner(GameResult result)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine();
        Console.WriteLine($"\n The WINNER of the Tournament is {result.Winner} with {result.Score} points");
        Console.WriteLine();
    }

}
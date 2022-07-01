namespace DominoLibrary;

public partial class GamePrinter
{
    public void AddTournament(Tournament tournament)
    {
        Tournament = tournament;
    }

    public void ShowTournamentStatus(int round, Dictionary<Team, int> scores)
    {
        Console.Clear();
        Console.WriteLine("\nScore status");

        foreach (var item in scores)
        {
            if (item.Equals(scores.Last()))
            {
                Console.Write($"{item.Key}: {item.Value} points");
            }
            else Console.Write($"{item.Key}: {item.Value} points   |   ");
        }
        Console.WriteLine();
        
        Console.WriteLine($"\nRound #{round} is about to begin");
    }

    public void PrintTournamentWinner(GameResult result)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine();

        try
        {
            Console.WriteLine($"\n{result.Winner} win this tournament with {result.Score} points"); 
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("\nTie Game");
        }
    
        Console.WriteLine();
    }

}
using System.Diagnostics;
namespace ConsoleApp;

public class QuickScreen
{
    string Info;
    int Seconds = 5;
    long SecondsToPrint;

    public QuickScreen(string info) => Info = info;
    public QuickScreen(string info, int seconds) : this(info) => Seconds = seconds;

    public void Show()
    {
        Stopwatch crono = new Stopwatch();

        crono.Start();
        
        while (crono.ElapsedMilliseconds < Seconds*1000)
        {
            long temp = crono.ElapsedMilliseconds;

            if (temp %1000 == 0)
            {
                SecondsToPrint = Seconds - temp/1000;
                Print();
            }
        }

        crono.Stop();
    }
    private void Print()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkRed;
        System.Console.WriteLine("\n === Information ===");

        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("\n");
        System.Console.WriteLine(Info);

        Console.ForegroundColor = ConsoleColor.White;
        System.Console.WriteLine($"\n this messagge will desapear in {SecondsToPrint}...");
    }
}
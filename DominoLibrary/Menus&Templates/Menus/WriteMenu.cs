using Utils;
using DominoLibrary;

namespace ConsoleApp;

public class WriteMenu
{
    public string Title { get; private set; }
    public int Selected { get; private set; }

    public WriteMenu(string title)
    {
        Title = title;
    }
    public void Show()
    {
        string writed = "";

        do
        {
            Print();

            writed = Console.ReadLine()!;
            
        } while (!IsValid(writed));

        Selected = int.Parse(writed);
        return;
    }
    private bool IsValid(string writed)
    {
        try
        {
            int.Parse(writed);
            return true;
        }
        catch (Exception)
        { 
            return false;
        }
    }
    private void Print()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        System.Console.WriteLine("\n");
        System.Console.WriteLine($"==== {Title} ====\n");
        Console.ForegroundColor = ConsoleColor.Gray;

        System.Console.Write("Write number and press ");
        Console.ForegroundColor = ConsoleColor.Blue;
        System.Console.WriteLine("ENTER");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
using DominoLibrary;
using System.Runtime.InteropServices;
using Utils;

namespace ConsoleApp;

class Program
{
    public static void Main()
    {
        Console.Title = "Dommie";

        // resize windows in WindowsOS case
        if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.SetWindowSize( 110, 30 );
        }

        SayHiDommie();

        Utils.Utils.Lapse(5); 

        MainSkipIntro();
    }
    public static void MainSkipIntro()
    {
        ConsoleApp menu = new ConsoleApp();
        Console.ForegroundColor = ConsoleColor.White;
    }
    
    private static void SayHiDommie()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkCyan;

        Console.Write("\n");
        Console.Write("\n");
        Console.Write("\n");
        Console.Write("\n");
        Console.Write("\n");
        Console.Write("\n");
        Console.Write("\n");
        Console.Write("\n");
        Console.Write("\n");
        Console.Write("\n");
        Console.Write("                OOOOOOO        OOOOO     OOOO       OOOO   OOOO       OOOO   OOO   OOOOOOOOO      \n");
        Console.Write("                OOO  OOO     OOO   OOO   OOO OOO OOO OOO   OOO OOO OOO OOO   OOO   OOO            \n");
        Console.Write("                OOO   OOO    OOO   OOO   OOO  OOOO   OOO   OOO  OOOO   OOO   OOO   OOO            \n");
        Console.Write("                OOO    OOO   OOO   OOO   OOO   OO    OOO   OOO   OO    OOO   OOO   OOOOOO         \n");
        Console.Write("                OOO    OOO   OOO   OOO   OOO         OOO   OOO         OOO   OOO   OOOOOOO        \n");
        Console.Write("                OOO   OOO    OOO   OOO   OOO         OOO   OOO         OOO   OOO   OOO            \n");
        Console.Write("                OOO  OOO     OOO   OOO   OOO         OOO   OOO         OOO   OOO   OOO            \n");
        Console.Write("                OOOOOOO        OOOOO     OOO         OOO   OOO         OOO   OOO   OOOOOOOOOO     \n");

        Console.ForegroundColor = ConsoleColor.White;
    }

}



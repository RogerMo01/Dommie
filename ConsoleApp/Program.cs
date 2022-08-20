using DominoLibrary;
using System.Runtime.InteropServices;
using Utils;

namespace ConsoleApp;

class ConsoleApp
{
    
    public static void Main()
    {
        Console.Title = "Dommie";

        if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.SetWindowSize( 110, 30 );
        }

        SayHiDommie();

        Lapse l = new Lapse(5);

        MainSkipIntro();
    }
    public static void MainSkipIntro()
    {
        MenuExplorer menu = new MenuExplorer();
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

class MenuExplorer
{
    List<IStrategy> Strategies = new List<IStrategy>()
    {
        new BotaGorda(),
        new Mosaic(),
        new Random_Strategy()
    };


    bool HumanPlay;
    bool JustBoardGame;
    ITemplate Template;
    IGame Game;

    public MenuExplorer()
    {
        HumanPlay = Menus.HumanPlayMenu();

        JustBoardGame = Menus.GameModeMenu();

        ITemplate custom = new CustomTemplate();
        Template = Menus.TemplateMenu(Strategies, custom, HumanPlay);

        // Customize Menu Option
        if(Template.Equals(custom))
        {
            bool agreeCustomization = false;
            do
            {
                CustomizeGame customizer = new CustomizeGame(Strategies, HumanPlay);
                Template = customizer.Start(ref JustBoardGame);

                agreeCustomization = Menus.MakeSureCostumizationMenu();

            } while (!agreeCustomization);
        }

        // READY FOR LAUNCH ~~~~~~~~~~
        Game = (JustBoardGame) ? Template.Board : Template.Tournament;

        GamePrinter gp = new GamePrinter();
        Game.SetGamePrinter(gp); // attach printer
        
        QuickScreen quickScreen = new("All set, the game will start soon");
        quickScreen.Show();
        
        Game.RunRound();

        Console.WriteLine("Press any key to end game");
        Console.ReadKey();

        if(PlayAgainMenu()) { ConsoleApp.MainSkipIntro(); }
    }

    private bool PlayAgainMenu()
    {
        SimpleOption playAgainOption = new SimpleOption("Play again");
        SimpleOption quitOption = new SimpleOption("Quit");

        List<SimpleOption> options = new List<SimpleOption>(){ playAgainOption, quitOption };
        SingleSelectionMenu<SimpleOption> menu = new SingleSelectionMenu<SimpleOption>(options, "THANKS FOR PLAYING DOMMIE, now what? ", false);
        menu.Show();

        return menu.Selected.Equals(playAgainOption);
    }

}

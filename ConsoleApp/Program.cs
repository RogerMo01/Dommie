using DominoLibrary;
using Utils;

namespace ConsoleApp;

class ConsoleApp
{
    
    public static void Main()
    {
        Console.Title = "Dommie";
        Console.SetWindowSize( 110, 30 );
        SayHiDommie();

        Lapse l = new Lapse(5);

        MenuExplorer menu = new MenuExplorer();
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
        Console.Write("                OOOOOOO        OOOOO     OOOO       OOOO   OOOO       OOOO   OOO   OOOOOOOOOOO    \n");
        Console.Write("                OOO  OOO     OOO   OOO   OOO OOO OOO OOO   OOO OOO OOO OOO   OOO   OOO            \n");
        Console.Write("                OOO   OOO    OOO   OOO   OOO  OOOO   OOO   OOO  OOOO   OOO   OOO   OOO            \n");
        Console.Write("                OOO    OOO   OOO   OOO   OOO   OO    OOO   OOO   OO    OOO   OOO   OOOOOOO        \n");
        Console.Write("                OOO    OOO   OOO   OOO   OOO         OOO   OOO         OOO   OOO   OOOOOOO        \n");
        Console.Write("                OOO   OOO    OOO   OOO   OOO         OOO   OOO         OOO   OOO   OOO            \n");
        Console.Write("                OOO  OOO     OOO   OOO   OOO         OOO   OOO         OOO   OOO   OOO            \n");
        Console.Write("                OOOOOOO        OOOOO     OOO         OOO   OOO         OOO   OOO   OOOOOOOOOOO    \n");
        Console.Write("\n");
        Console.Write("\n");
        Console.Write("\n");
        Console.Write("\n");
        Console.Write("\n");

        Console.ForegroundColor = ConsoleColor.Black;
    }
}

class MenuExplorer
{
    List<IStrategy> Strategies = new List<IStrategy>()
    {
        new BotaGorda(),
        new Mosaic()
    };

    bool HumanPlay;
    bool JustBoardGame;
    int BaseMaxToken = 6;
    bool SinglePlayer;
    List<Team> Teams = new();
    ITemplate Template;
    IGame Game;

    public MenuExplorer()
    {
        HumanPlay = Menus.HumanPlayMenu();

        JustBoardGame = Menus.GameModeMenu();

        SinglePlayer = Menus.SinglePlayerOrTeamMenu();

        ITemplate custom = new CustomTemplate();
        Template = Menus.TemplateMenu(Strategies, custom, SinglePlayer);

        if(Template.Equals(custom))
        {
            bool agreeCustomization = false;
            do
            {
                CustomizeGame customizer = new CustomizeGame(Strategies, HumanPlay, JustBoardGame, SinglePlayer, Teams);
                Template = customizer.Start();

                agreeCustomization = Menus.MakeSureCostumizationMenu();

            } while (!agreeCustomization);
        }

        // READY FOR LAUNCH ~~~~~~~~~~
        Game = (JustBoardGame) ? Template.Board : Template.Tournament;

        GamePrinter gp = new GamePrinter();
        Game.SetGamePrinter(gp); // attach observer
        
        QuickScreen quickScreen = new("All set =), the game will start soon");
        quickScreen.Show();
        
        Game.Start();
    }

}

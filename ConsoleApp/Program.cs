using DominoLibrary;
using Utils;

namespace ConsoleApp;

class ConsoleApp
{
    
    public static void Main()
    {
        Console.Title = "Dommie";

        //~~~~~~~~~~~~~~~~~~~~~~~~~~ MENU ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        MenuExplorer menu = new MenuExplorer();
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
    List<Team> Teams;
    ITemplate Template;
    IGame Game;

    public MenuExplorer()
    {
        HumanPlay = Menus.HumanPlayMenu();

        JustBoardGame = Menus.GameModeMenu();

        // Teams (empty)
        Teams = new List<Team>();
        SinglePlayer = Menus.SinglePlayerOrTeamMenu();

        ITemplate custom = new CustomTemplate();
        Template = Menus.TemplateMenu(Strategies, custom, Teams, SinglePlayer);

        if(Template.Equals(custom))
        {
            bool agreeCustomization = false;
            do
            {
                CustomizeGame();
                agreeCustomization = Menus.MakeSureCostumizationMenu();

            } while (!agreeCustomization);
        }

        // READY FOR LAUNCH ~~~~~~~~~~
        Game = (JustBoardGame) ? Template.Board : Template.Tournament;

        GamePrinter gp = new GamePrinter();
        Game.SetGamePrinter(gp); // attach observer
        
        Game.Start();
    }

    private void CustomizeGame()
    {
        // DEFAULT PARAMETERS
        List<IPlayer> playersSelection;
        int maxTokenSelection = BaseMaxToken;
        int scoreSelection = 100;
        WinBoard overBoardConditionSelection = BoardWins.ClassicWinBoard;
        WinnerBoard winnerBoardGetterSelection = BoardWinners.ClassicGetWinner;

        // NUMBER PLAYERS MENU ~~~~~~
        int NumberPlayers = Menus.NumberPlayersMenu();
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~

        // Generar players
        playersSelection = TemplateUtils.GeneratePlayers(NumberPlayers, Strategies);
        
        if(!HumanPlay)
        {
            // PLAYERS MENU ~~~~~~
            playersSelection = Menus.CustomizePlayersMenu(playersSelection, Strategies, NumberPlayers);
            // ~~~~~~~~~~~~~~~~~~~
        }

        // CUSTOM TEAM MENU !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        Teams = Menus.AssignTeamsMenu(playersSelection, SinglePlayer);
        // ~~~~~~~~~~~~~~~~~~~~~~
        
        // MAX TOKEN MENU ~~~~~~
        maxTokenSelection = Menus.MaxTokenMenu(BaseMaxToken);
        // ~~~~~~~~~~~~~~~~~~~~~


        if(!JustBoardGame)
        {
            // WIN SCORE SELECTION ~~~~~~~~~
            WriteMenu scoreWriteMenu = new WriteMenu("DECIDE AND WRITE THE NEEDED POINTS TO WIN THE TOURNAMENT");
            scoreWriteMenu.Show();

            scoreSelection = scoreWriteMenu.Selected;
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }

        // WIN BOARD JUDGMENT ~~~~~~~~~~~~~~~~~~~~
        overBoardConditionSelection = Menus.OverConditionMenu();
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        // GET WINNER JUDGMENT ~~~~~~~~~~~~
        winnerBoardGetterSelection = Menus.GetWinnerMenu();
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        Template = TemplateUtils.BuildTemplate(TemplateUtils.ToCircularList(playersSelection), maxTokenSelection, NumberPlayers, scoreSelection, overBoardConditionSelection, winnerBoardGetterSelection, Teams);
    }
}
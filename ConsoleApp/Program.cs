using DominoLibrary;
using Utils;

namespace ConsoleApp;

class ConsoleApp
{
    
    public static void Main()
    {
        Console.Title = "Dommie";

        //~~~~~~~~~~~~~~~~~~~~~~~~~~ MENU ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //default
        int numberOfPlayers = 3;

        // Instanciar jugadores
        CircularList<IPlayer> players = new CircularList<IPlayer>(new Player("Dustin", new List<IStrategy>(){new BotaGorda()}));
        players.AddLast(new Player("Eleven", new List<IStrategy>(){new BotaGorda()}));
        players.AddLast(new Player("Mike", new List<IStrategy>(){new BotaGorda()}));
        players.AddLast(new Player("Lucas", new List<IStrategy>(){new BotaGorda()}));

        // default
        int maxToken = 6;

        // default
        int winScore = 100;

        // Instanciar inner
        Node<IPlayer> inner = players.First;

        // Instanciar metodo de WinBoard
        WinBoard meth = new WinBoard(BoardWins.ClassicWinBoard);
        WinBoard meth2 = new WinBoard(BoardWins.CrazyTokenWinBoard);

        WinnerBoard methW = new WinnerBoard(BoardWinners.FewerTokens);

        Judge judge = new Judge(meth, methW);

        
        List<Team> team = new List<Team>();
        List<IPlayer> team1 = new List<IPlayer>{players.Last.Previous!.Value};
        team.Add(new Team(team1));
        List<IPlayer> team2 = new List<IPlayer>{players.First.Next!.Value};
        team.Add(new Team(team2));
        List<IPlayer> team3 = new List<IPlayer>{players.Last.Value};
        team.Add(new Team(team3));
        List<IPlayer> team4 = new List<IPlayer>{players.First.Value};
        team.Add(new Team(team4));

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        Tournament tournament = new Tournament(new TournamentSetting(players, maxToken, numberOfPlayers, winScore, judge, team));
        //Board board = new Board(new BoardSetting(players, players.First, ))

        GamePrinter gp = new GamePrinter();
        tournament.SetGamePrinter(gp); // attach observer
        
        // MAIN CALL
        //tournament.Start();

        //======================================================================================================
        //ShowMenus();
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
    int NumberPlayers = 4;
    int BaseMaxToken = 6;
    ITemplate Template;
    IGame Game;

    public MenuExplorer()
    {
        HumanPlay = Menus.HumanPlayMenu();

        JustBoardGame = Menus.GameModeMenu();

        ITemplate custom = new CustomTemplate();
        Template = Menus.TemplateMenu(NumberPlayers, Strategies, custom);

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
        CircularList<IPlayer> playersSelection = TemplateUtils.GeneratePlayers(4, Strategies);
        int maxTokenSelection = BaseMaxToken;
        int scoreSelection = 100;
        WinBoard overBoardConditionSelection = BoardWins.ClassicWinBoard;
        WinnerBoard winnerBoardGetterSelection = BoardWinners.ClassicGetWinner;

        if(!HumanPlay)
        {
            // PLAYERS MENU ~~~~~~~~~~~~~~~~~~~~~~~~~~~
            CustomPlayersMenu customPlayers = new CustomPlayersMenu(NumberPlayers, playersSelection, Strategies);
            customPlayers.Show();

            playersSelection = customPlayers.Players;
        }
        
        // MAX TOKEN MENU ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        maxTokenSelection = Menus.MaxTokenMenu(BaseMaxToken);


        if(!JustBoardGame)
        {
            // WIN SCORE SELECTION ~~~~~~~~~~~~~~~~~~~
            WriteMenu scoreWriteMenu = new WriteMenu("DECIDE AND WRITE THE NEEDED POINTS TO WIN THE TOURNAMENT");
            scoreWriteMenu.Show();

            scoreSelection = scoreWriteMenu.Selected;
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }

        // WIN BOARD JUDGMENT ~~~~~~~~~~~~~~~~~~~~
        overBoardConditionSelection = Menus.OverConditionMenu();
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        // GET WINNER JUDGMENT ~~~~~~~~~~~~
        winnerBoardGetterSelection = Menus.GetWinnerMenu();
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        Template = TemplateUtils.BuildTemplate(playersSelection, maxTokenSelection, scoreSelection, overBoardConditionSelection, winnerBoardGetterSelection);
    }
}
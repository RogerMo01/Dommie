using DominoLibrary;
using Utils;

namespace ConsoleApp;

static class ConsoleApp
{

    public static void Main()
    {
        //~~~~~~~~~~~~~~~~~~~~~~~~~~ MENU ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //default
        int numberOfPlayers = 3;

        // Instanciar jugadores
        CircularList<IPlayer> players = new CircularList<IPlayer>(new Player("Dustin", new List<IStrategy>(){new Mosaic()}));
        players.AddLast(new Player("Eleven", new List<IStrategy>(){new Mosaic()}));
        players.AddLast(new Player("Mike", new List<IStrategy>(){new Mosaic()}));
        players.AddLast(new Player("Lucas", new List<IStrategy>(){new Mosaic()}));

        // default
        int maxToken = 6;

        // default
        int winScore = 100;

        // Instanciar inner
        Node<IPlayer> inner = players.First;

        // Instanciar metodo de WinBoard
        WinBoard meth = new WinBoard(BoardWins.ClassicWinBoard);
        WinBoard meth2 = new WinBoard(BoardWins.CrazyTokenWinBoard);

        WinnerBoard methW = new WinnerBoard(BoardWinners.ClassicGetWinner);

        Judge judge = new Judge(meth2, methW);

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        Tournament tournament = new Tournament(new TournamentSetting(players, maxToken, numberOfPlayers, winScore, judge));
        //Board board = new Board(new BoardSetting(players, players.First, ))

        GamePrinter gp = new GamePrinter();
        tournament.SetGamePrinter(gp); // attach observer
        
        // MAIN CALL
        tournament.Start();



        //======================================================================================================
        //ShowMenus();
    }

    private static void ShowMenus()
    {
        // MENU (HUMAN - PC)
        SimpleOption humanOption = new SimpleOption("I will play");
        SimpleOption pcOption = new SimpleOption("Only PC will play");
        List<SimpleOption> hpSelectionables = new List<SimpleOption>(){ humanOption, pcOption };

        SingleSelectionMenu hPcMenu = new SingleSelectionMenu(hpSelectionables, "WHAT DO YOU LIKE TO DO");
        hPcMenu.Show();

        bool m_HumanPlay = (hPcMenu.Selected.Equals(humanOption));

        
        // MENU (TOURNAMENT - BOARD)
        SimpleOption boardOption = new SimpleOption("Single Play");
        SimpleOption tournamentOption = new SimpleOption("Tournament Play");
        List<SimpleOption> tbSelectionables = new List<SimpleOption>(){ boardOption, tournamentOption };

        SingleSelectionMenu btMenu = new SingleSelectionMenu(tbSelectionables, "SELECT A GAME MODE");
        btMenu.Show();

        bool m_GameMode = (hPcMenu.Selected.Equals(humanOption));


        // MENU (TEMPLATE)



    }







}
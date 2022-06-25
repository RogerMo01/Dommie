using DominoLibrary;
using Utils;

namespace ConsoleApp;

static class ConsoleApp
{

    public static void Main()
    {
        //~~~~~~~~~~~~~~~~~~~~~~~~~~ MENU ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        Tournament tournament = new Tournament(new TournamentSetting(players, maxToken, winScore, meth2, methW));

        GamePrinter gp = new GamePrinter();
        tournament.SetGamePrinter(gp); // attach observer
        
        // MAIN CALL
        tournament.Start();

    }




}
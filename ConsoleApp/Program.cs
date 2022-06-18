using DominoLibrary;
using Utils;

namespace ConsoleApp;

static class ConsoleApp
{
    public static void Main()
    {
        // Instanciar jugadores
        CircularList<IPlayer> players = new CircularList<IPlayer>(new Player("Dustin", new List<IStrategy>(){new Mosaic()}));
        players.AddLast(new Player("Eleven", new List<IStrategy>(){new Mosaic()}));
        players.AddLast(new Player("Mike", new List<IStrategy>(){new Mosaic()}));
        players.AddLast(new Player("Lucas", new List<IStrategy>(){new Mosaic()}));

        // default
        int maxToken = 6;

        // Instanciar inner
        Node<IPlayer> inner = players.First;


        Setting initialSetting = new Setting(players, maxToken, inner);

        Board game = new Board(initialSetting);

        ShowGame(game);
    }

    private static void ShowGame(Board game)
    {
        game.Start();
    }
}
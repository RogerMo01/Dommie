using Utils;
using DominoLibrary;

namespace ConsoleApp;

class CustomPlayersMenu
{
    string Title = "CUSTOMIZE PLAYERS";
    public CircularList<IPlayer> Players { get; private set; }
    List<IStrategy> Strategies;
    Node<IPlayer> CurrentPlayer;
    List<IPlayer> NewPlayers = new List<IPlayer>();

    public CustomPlayersMenu(int numberPlayers, CircularList<IPlayer> players, List<IStrategy> strats)
    {
        Players = players;
        Strategies = strats;
        CurrentPlayer = Players.First;
    }

    public void Show()
    {
        ConsoleKey pressedKey = ConsoleKey.D1;

        for (int i = 0; i < Players.Count; i++)
        {
            // validate election
            int startPoint = 49;
            int selectionNumber = startPoint;

            do
            {
                Console.Clear();
                Print();

                pressedKey = Console.ReadKey().Key;
                selectionNumber = (int)pressedKey;


            } while ((selectionNumber > startPoint + (Strategies.Count-1) || selectionNumber < startPoint));
            

            //if(selectionNumber > startPoint + (Strategies.Count-1) || selectionNumber < startPoint) return;
            // ~~~~~~~~~~~~~~~~~

            switch (selectionNumber)
            {
                case 49:
                NewPlayers.Add(new Player(CurrentPlayer.Value.Name, new List<IStrategy>(){new BotaGorda()}));
                break;

                case 50:
                NewPlayers.Add(new Player(CurrentPlayer.Value.Name, new List<IStrategy>(){new Mosaic()}));
                break;

                default:
                break;
            }

            CurrentPlayer = CurrentPlayer.Next!;
        }

        
    }

    private void Print()
    {
        // Title
        Console.ForegroundColor = ConsoleColor.Blue;
        System.Console.WriteLine("\n");
        System.Console.WriteLine($"===== {Title} =====\n");
        Console.ForegroundColor = ConsoleColor.White;

        System.Console.WriteLine($"Customize {CurrentPlayer.Value.Name}");
        for (int i = 0; i < Strategies.Count; i++)
        {
            System.Console.Write($"[{i+1}]  {Strategies[i]}\n"); 
        }
        System.Console.WriteLine("\n Press the number option to select it");
    }
}
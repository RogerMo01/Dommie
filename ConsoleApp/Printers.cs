using Utils;
using DominoLibrary;
namespace ConsoleApp;

class ConsolePrinter
{
    # region Round Printers
        public static void ShowPlayerTokens(GameStatus gameStatus, Dictionary<IPlayer, ConsoleColor> colors)
        {
            Console.WriteLine();
            Console.WriteLine("Tokens hand out was:");
            
            foreach (var item in gameStatus.PlayersTokens!)
            {
                Console.ForegroundColor = colors[item.Key];
                Console.WriteLine($"{item.Key.Name}:");

                foreach (var token in item.Value)
                {
                    Console.Write($"[{token.Left}:{token.Right}] ");
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void PrintPlay(GameStatus gameStatus, Dictionary<IPlayer, ConsoleColor> colors)
        {
            if(gameStatus.Players!.ToArray().Any(x => x is HumanPlayer))
            {
                Console.Clear();
            }
            
            IPlay play = gameStatus.Plays!.Last();
            IPlayer player = play.Owner;

            Console.WriteLine();
            if(play is Pass)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\n{player.Name} pass");
            }
            else
            {
                string side = (play.PlayRight) ? "Right" : "Left";

                Console.ForegroundColor = colors[play.Owner];
                Console.Write($"\n{play.Owner}");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(" played ");
                Console.ForegroundColor = colors[play.Owner];
                Console.Write(play);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($" by {side}");

                Console.ForegroundColor = ConsoleColor.White;
            }
            
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintRoundWinner(Team winner, int score)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("==== ROUND OVER ====");

            Console.ForegroundColor = ConsoleColor.Yellow;
            
            if(winner == null) 
            {
                Console.WriteLine("\nTie Game");
            }
            else
            {
                Console.WriteLine($"{winner} Win this round with {score} points");
            }
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    # endregion

    # region Tournament Printers
        
        public static void ShowTournamentStatus(int round, Dictionary<Team, int> scores)
        {
            Console.Clear();
            Console.WriteLine("\nScore status");

            foreach (var item in scores)
            {
                if (item.Equals(scores.Last()))
                {
                    Console.Write($"{item.Key}: {item.Value} points");
                }
                else Console.Write($"{item.Key}: {item.Value} points   |   ");
            }
            Console.WriteLine();
            
            Console.WriteLine($"\nRound #{round} is about to begin");
        }

        public static void PrintTournamentWinner(WinnerStatus result)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();

            try
            {
                Console.WriteLine($"\n{result.Winner} win this tournament with {result.Score} points"); 
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("\nTie Game");
            }
        
            Console.WriteLine();
        }

    # endregion


}
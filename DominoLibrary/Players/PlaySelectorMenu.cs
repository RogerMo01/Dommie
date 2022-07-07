namespace DominoLibrary;

public class PlaySelectorMenu
{
    public List<Token> Selectionables { get; }
    public Token Selected { get; private set; }
    public string Title { get; private set; } = " +++ Choose a Token to play +++";
    public int SelectedIndex { get; private set; } = 0;
    Board Board;

    public PlaySelectorMenu(List<Token> selectionables, Board board)
    {
        Selectionables = selectionables;
        Selected = Selectionables.First();
        Board = board;
    }

    public void Modify(ConsoleKey key)
    {
        int selectionNumber = (int)key;

        int upArrow = 38;
        int downArrow = 40;

        if(selectionNumber != upArrow && selectionNumber != downArrow) { return; /*no es valido*/}
        
        if(selectionNumber == upArrow)
        {
            if(Selected!.Equals(Selectionables.First())) return;

            SelectedIndex--;
            Selected = Selectionables[SelectedIndex];
        }

        if(selectionNumber == downArrow)
        {
            if(Selected!.Equals(Selectionables.Last())) return;

            SelectedIndex++;
            Selected = Selectionables[SelectedIndex];
        }
    }

    public void Show()
    {
        ConsoleKey pressedKey = ConsoleKey.D1;

        while ((int)pressedKey != (int)ConsoleKey.Enter)
        {
            Print();

            pressedKey = Console.ReadKey().Key;

            Modify(pressedKey);
        }
        return;
    }
    private void Print()
    {
        Console.Clear();

        // Title
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\n");
        Console.WriteLine($" {Title} \n");

        // Options
        for (int i = 0; i < Selectionables.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Write( (i<9) ? $"[ {i+1}]  " : $"[{i+1}]  ");
            if(Selectionables[i]!.Equals(Selected)){
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("->");
            }
            else{
                Console.Write(" ");
            }
            Console.WriteLine(" " + Selectionables[i]);
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"\nSelect a Token and press ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("ENTER");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" to play it\n");

        PrintBoard();
    }

    private void PrintBoard()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\n Board Tokens:");
        Console.WriteLine("");

        foreach (var item in Board.BoardTokens)
        {
            Console.ForegroundColor = item.Owner.Color;
            Console.Write(item);
        }

        Console.WriteLine("\n");

        IPlayer[] players = Board.Players.ToArray();

        foreach (var player in players)
        {
            Console.ForegroundColor = player.Color;
            Console.WriteLine(player);
        }
        Console.WriteLine("\n");

        PrintLastPlays();        
    }
    private void PrintLastPlays()
    {
        int lastPlays = Math.Min(Board.Players.Count * 2, Board.Plays.Count);
        List<(IPlayer player, Token_onBoard token)> lastPlaysList = new();

        for (int i = Board.Plays.Count - 1; i >= Board.Plays.Count - lastPlays; i--)
        {
            lastPlaysList.Add(Board.Plays[i]);
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"=== Last {lastPlays} Plays: ===");

        foreach (var play in lastPlaysList)
        {
            if(play.token.IsPass())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{play.player} pass");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{play.player} play {play.token}");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;

    }


}
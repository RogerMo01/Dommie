using Utils;
using DominoLibrary;
namespace ConsoleApp;

public class SingleSelectionMenu<T>
{
    public List<T> Selectionables { get; }
    public T Selected { get; private set; }
    public string Title { get; private set; }
    public int SelectedIndex { get; private set; } = 0;
    bool ContinueOption;


    public SingleSelectionMenu(List<T> selectionables, string title, bool continueOption)
    {
        Selectionables = selectionables;
        Selected = Selectionables.First();
        Title = title;
        ContinueOption = continueOption;
    }

    public void Modify(ConsoleKey key)
    {
        int selectionNumber = (int)key;

        int upArrow = 38;
        int downArrow = 40;
        int rightArrow = 39;
        int leftArrow  = 37;
        
        if(selectionNumber != upArrow && selectionNumber != downArrow && selectionNumber != leftArrow && selectionNumber != rightArrow)
        {
            return; // not valid
        }

        // only work with the left and right arrows in case of being in continue
        if(Selected == null && selectionNumber != leftArrow && selectionNumber != rightArrow) return;

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


        if (ContinueOption)
        {
            if(selectionNumber == rightArrow)
            {
                Selected = default(T)!; // null
                SelectedIndex = -1;
            }
            if(selectionNumber == leftArrow && Selected == null)
            {
                Selected = Selectionables.Last();
                SelectedIndex = Selectionables.Count - 1;
            }
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
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n");
        Console.WriteLine($"===== {Title} =====\n");


        // Options
        for (int i = 0; i < Selectionables.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Write($"[{i+1}]  ");
            if(Selectionables[i]!.Equals(Selected)){
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("->");
            }   
            else{
                Console.Write(" ");
            }
            Console.WriteLine(" " + Selectionables[i]);

            Console.ForegroundColor = ConsoleColor.DarkGray;
        }
        
        if(ContinueOption)
        {
            if (Selected == null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine("\n                     -> Continue");

            }
            else
            {
                System.Console.WriteLine("\n                       Continue");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"\nPress ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("RIGHT ARROW");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" to select Continue option");
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"\nSelect an option and Press ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("ENTER");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($" to customize it\n");
    }
}
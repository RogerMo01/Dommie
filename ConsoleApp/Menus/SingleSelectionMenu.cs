using Utils;
using DominoLibrary;
namespace ConsoleApp;

class SingleSelectionMenu<T>
{
    public List<T> Selectionables { get; }
    public T Selected { get; private set; }
    public string Title { get; private set; }
    public int SelectedIndex { get; private set; } = 0;


    public SingleSelectionMenu(List<T> selectionables, string title)
    {
        Selectionables = selectionables;
        Selected = Selectionables.First();
        Title = title;
    }

    public void Modify(ConsoleKey key)
    {
        int selectionNumber = (int)key;

        int upArrow = 38;
        int downArrow = 40;
        
        if(selectionNumber != upArrow && selectionNumber != downArrow)
        {
            // no es valido
            return;
        }

        

        if (selectionNumber == upArrow)
        {
            if(Selected!.Equals(Selectionables.First())) return;

            SelectedIndex--;
            Selected = Selectionables[SelectedIndex];
        }
        else
        {
            if(Selected!.Equals(Selectionables.Last())) return;

            SelectedIndex++;
            Selected = Selectionables[SelectedIndex];
        }

    }
    

    public void Show()
    {
        ConsoleKey pressedKey = ConsoleKey.D1;

        while ((int)ConsoleKey.Enter != (int)pressedKey)
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
        Console.ForegroundColor = ConsoleColor.Blue;
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
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"\nSelect and Press ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("ENTER");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(" to continue\n");

    }
}
namespace ConsoleApp;

public interface ISelectionable
{
    public string Name { get; }
    // que masssss
}

public class SimpleOption : ISelectionable
{
    public string Name { get; }

    public SimpleOption(string name)
    {
        Name = name;
    }
}



// class HeadMenu
// {
//     public List<ISelectionable> Menus;

//     public HeadMenu()
//     {
        
//     }
// }

class SingleSelectionMenu
{
    public List<SimpleOption> Selectionables { get;}
    public SimpleOption Selected { get; private set; }
    string Title;

    public SingleSelectionMenu(List<SimpleOption> selectionables, string title)
    {
        Selectionables = selectionables;
        Selected = Selectionables.First();
        Title = title;
    }

    public void Select(ConsoleKey key, int numberOptions)
    {
        // D1 inicia en 49
        int startPoint = 49;
        int selectionNumber = (int)key;

        if(selectionNumber > startPoint + (numberOptions-1) || selectionNumber < startPoint)
        {
            // no es valido
            return;
        }

        Selected = Selectionables[selectionNumber-startPoint];
    }

    public void Show()
    {
        ConsoleKey pressedKey = ConsoleKey.D1;

        while ((int)ConsoleKey.Enter != (int)pressedKey)
        {
            Print();

            pressedKey = Console.ReadKey().Key;

            Select(pressedKey, Selectionables.Count);
        }
        return;
    }
    private void Print()
    {
        Console.Clear();

        // Title
        Console.ForegroundColor = ConsoleColor.Blue;
        System.Console.WriteLine($"===== {Title} =====\n");

        // Options
        for (int i = 0; i < Selectionables.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            System.Console.Write($"[{i+1}]  ");
            if(Selectionables[i].Equals(Selected)){
                Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write("->");
            }   
            else{
                System.Console.Write(" ");
            }
            System.Console.WriteLine(" " + Selectionables[i].Name);
        }

        Console.ForegroundColor = ConsoleColor.White;
        System.Console.WriteLine("\n Press ENTER to continue");
    }

}

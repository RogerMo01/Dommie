namespace ConsoleApp;

interface IMenu<inT>
{
    public List<inT> Options {get;}
    public string Title {get;}
    public int Selected {get;}
    public void GetSelection();
}

class NavigationMenu<inT> : IMenu<inT>
{
    public List<inT> Options {get; private set;}
    public string Title {get; private set;}
    public int Selected {get; private set;}

    public NavigationMenu(List<inT> options, string  title)
    {
        Options = options;
        Title = title;
        
        GetSelection();
    }

    public void GetSelection()
    {
        // Muestra el menu con las opciones para elegir
    }
}

class SelectionMenu<inT> : IMenu<inT>
{
    public List<inT> Options {get; private set;}
    public string Title {get; private set;}
    public int Selected {get; private set;}

    public SelectionMenu(List<inT> options, string  title)
    {
        Options = options;
        Title = title;

        GetSelection();
    }

    public void GetSelection()
    {
        // mostrar adem'as lo seleccionado
    }
}

class WriteMenu<inT> : IMenu<inT>
{
    public List<inT> Options {get; private set;}
    public string Title {get; private set;}
    public int Selected {get; private set;}

    public WriteMenu(string title)
    {
        Title = title;
        Options = new List<inT>();

        GetSelection();
    }

    public void GetSelection()
    {
        
    }
}
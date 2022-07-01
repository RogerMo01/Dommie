using DominoLibrary;
using Utils;
namespace ConsoleApp;

class SimpleOption
{
    public string Title { get; }
    public SimpleOption(string name)
    {
        Title = name;
    }

    public override string ToString() => this.Title;
}

class DelegateOption<T>
{
    public string Title { get; }
    public T Deleg { get; }
    public DelegateOption(T deleg, string name)
    {
        Title = name;
        Deleg = deleg;
    }

    public override string ToString() => this.Title;
}
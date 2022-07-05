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

class GenericOption<T>
{
    public string Title { get; }
    public T Value { get; }
    public GenericOption(T option, string name)
    {
        Title = name;
        Value = option;
    }

    public override string ToString() => this.Title;
}
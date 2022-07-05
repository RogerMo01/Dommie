using Utils;
using DominoLibrary;

namespace ConsoleApp;

class CustomTemplate : ITemplate
{
    public Board Board { get; private set; } = null!;
    public Tournament Tournament { get; private set; } = null!;
    public string Title { get; } = "Custom";

    public CustomTemplate(){}

    public CustomTemplate(Board board, Tournament tournament)
    {
        Board = board;
        Tournament = tournament;
    }

    public override string ToString() => "Customize your own template";
}
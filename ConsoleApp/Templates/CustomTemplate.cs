using Utils;
using DominoLibrary;

namespace ConsoleApp;

public class CustomTemplate : ITemplate
{
    public Round Round { get; private set; } = null!;
    public Tournament Tournament { get; private set; } = null!;
    public string Title { get; } = "Custom";

    public CustomTemplate(){}

    public CustomTemplate(Round round, Tournament tournament)
    {
        Round = round;
        Tournament = tournament;
    }

    public override string ToString() => "Customize your own template";
}
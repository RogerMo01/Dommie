using DominoLibrary;
namespace Utils;

public static class Utils
{
    public static Dictionary<IPlayer, ConsoleColor> AssignColors(IPlayer[] players)
    {
        ConsoleColor[] colors = {
            ConsoleColor.Blue,
            ConsoleColor.Cyan,
            ConsoleColor.Magenta,
            ConsoleColor.Green,
            ConsoleColor.DarkBlue,
            ConsoleColor.Yellow
        };

        Dictionary<IPlayer, ConsoleColor> assignation = new();

        for (int i = 0; i < players.Length; i++)
        {
            assignation.Add(players[i], colors[i]);
        }

        return assignation;
    }
}
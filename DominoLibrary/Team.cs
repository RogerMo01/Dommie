namespace DominoLibrary;
using Utils;

public class Team
{
    public List<IPlayer> PlayersTeam {get; private set; }
    public int Count { get => PlayersTeam.Count; }

    public Team(List<IPlayer> playersTeam)
    {
        PlayersTeam  = playersTeam;
    }

    public override string ToString()
    {
        string result = "";

        if(PlayersTeam.Count == 1)
        {
            result = PlayersTeam.First().Name;
        }
        else
        {
            result = "(";
            
            for (int i = 0; i < PlayersTeam.Count - 1; i++)
            {
                result += (PlayersTeam[i].Name) + " - ";
            }

            result += $"{PlayersTeam[PlayersTeam.Count - 1].Name})";
        }

        return result;
    }

    public bool Contains(IPlayer player)
    {
        foreach (var item in PlayersTeam)
        {
            if(item.Equals(player)) return true;
        }

        return false;
    }
}
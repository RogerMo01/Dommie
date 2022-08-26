using DominoLibrary;

namespace ConsoleApp;

public partial class Menus
{
    public static bool SinglePlayerOrTeamMenu()
    {
        SimpleOption singlePlayer = new SimpleOption("Single player Play");
        List<SimpleOption> options = new List<SimpleOption>(){ singlePlayer, new SimpleOption("Play with Teams")};

        SingleSelectionMenu<SimpleOption> teamsMenu = new SingleSelectionMenu<SimpleOption>(options, "TEAM PLAY", false);
        teamsMenu.Show();

        return teamsMenu.Selected.Equals(singlePlayer);
    }
    public static List<Team> AssignTeamsMenu(List<IPlayer> players, bool singlePlayer, bool humanPlay)
    {
        if(players.Count < 3)
        {
            QuickScreen q = new QuickScreen("No Teams Game allowed for less than 3 Players", 4);
            q.Show();
            return GenerateUnitaryTeams(players, humanPlay); // only modifiable from (3-6) players
        }

        List<Team> teams = new List<Team>();

        if(singlePlayer)
        {
            return GenerateUnitaryTeams(players, humanPlay);
        }
        
        return CustomizeTeams(players);
    }

    public static List<Team> GenerateUnitaryTeams(List<IPlayer> players, bool humanPlay)
    {
        List<Team> teams = new List<Team>();
        for (int i = 0; i < players.Count; i++)
        {
            teams.Add( new Team(new List<IPlayer>(){ players[i] }) );
        }

        return teams;
    }

    private static List<Team> CustomizeTeams(List<IPlayer> players)
    {
        int numberOfPlayers = players.Count;
        int currentTeamIndex = 1;
        int numberOfTeams = NumberTeamsMenu(numberOfPlayers);


        List<Team> teams = new List<Team>();
        List<IPlayer> tempTeam = new List<IPlayer>();


        // make menu with players as options 
        List<GenericOption<IPlayer>> options = new();

        for (int j = 0; j < numberOfPlayers; j++)
        {
            options.Add( new GenericOption<IPlayer>(players[j], players[j].Name));
        }


        // for each players
        for (int j = 0; j < numberOfPlayers; j++)
        {
            // same numbers of players and left teams (Autocomplete)
            if((numberOfTeams - currentTeamIndex) == (numberOfPlayers - j))
            {
                List<IPlayer> restPlayers = SetAsList(options);
                teams.Add(new Team(tempTeam));

                List<Team> restTeam = GenerateUnitaryTeams(restPlayers, false);
                for (int k = 0; k < restTeam.Count; k++)
                {
                    teams.Add(restTeam[k]);
                }

                QuickScreen q = new QuickScreen("Autocompleting teams", 4);
                q.Show();
                break;
            }

            // only one team left (Autocomplete)
            if (currentTeamIndex == numberOfTeams)
            {
                List<IPlayer> restPlayers = SetAsList(options);
                teams.Add(new Team(restPlayers));

                QuickScreen q = new QuickScreen("Autocompleting teams", 5);
                q.Show();
                break;
            }

            SingleSelectionMenu<GenericOption<IPlayer>> menu = new(options, $"ADD PLAYERS TO: Team #{currentTeamIndex}", true);
            menu.Show();

            if(menu.Selected == null && tempTeam.Count == 0){ j--; continue; }
            
            if(menu.Selected == null)
            { 
                teams.Add(new Team(tempTeam));
                tempTeam = new();
                currentTeamIndex++;
                j--;
            }
            else
            {
                options.RemoveAt(menu.SelectedIndex);
                tempTeam.Add(menu.Selected.Value);
            }
        }

        return teams;
    }

    private static List<IPlayer> SetAsList(List<GenericOption<IPlayer>> options)
    {
        List<IPlayer> restPlayers = new List<IPlayer>();
        for (int j = 0; j < options.Count; j++)
        {
            restPlayers.Add(options[j].Value);
        }

        return restPlayers;
    }

    private static int NumberTeamsMenu(int numberOfPlayers)
    {
        int min = 2;
        int max = numberOfPlayers - 1;

        if(numberOfPlayers == 3) return 2;

        List<GenericOption<int>> options = new List<GenericOption<int>>();
        for (int i = min; i <= max; i++)
        {
            options.Add(new GenericOption<int>(i, $"{i} Teams"));
        }

        SingleSelectionMenu<GenericOption<int>> menu = new SingleSelectionMenu<GenericOption<int>>(options, "CHOOSE NUMBER OF TEAMS", false);
        menu.Show();
        
        return menu.Selected.Value;
    }
}
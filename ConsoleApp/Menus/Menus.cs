using Utils;
using DominoLibrary;

namespace ConsoleApp;

static class Menus
{
    public static bool HumanPlayMenu()
    {
        // Options
        SimpleOption humanOption = new SimpleOption("I will play");
        SimpleOption pcOption = new SimpleOption("Only PC will play");
        List<SimpleOption> hpSelectionables = new List<SimpleOption>(){ humanOption, pcOption };
        // ...

        SingleSelectionMenu<SimpleOption> menu = new SingleSelectionMenu<SimpleOption>(hpSelectionables, "WHAT DO YOU LIKE TO DO");
        menu.Show();

        return menu.Selected.Equals(humanOption);
    }

    public static bool GameModeMenu()
    {
        // Options
        SimpleOption boardOption = new SimpleOption("Single Play");
        SimpleOption tournamentOption = new SimpleOption("Tournament Play");
        // ...

        List<SimpleOption> tbSelectionables = new List<SimpleOption>(){ boardOption, tournamentOption };
        SingleSelectionMenu<SimpleOption> menu = new SingleSelectionMenu<SimpleOption>(tbSelectionables, "SELECT A GAME MODE");
        menu.Show();

        return menu.Selected.Equals(boardOption);
    }

    public static ITemplate TemplateMenu(int numberPlayers, List<IStrategy> strategies, ITemplate custom, List<Team> teams)
    {
        


        // Options
        ITemplate classic_9 = new ClassicTemplate("Classic double-9", numberPlayers, 9, strategies, teams);
        ITemplate classic_6 = new ClassicTemplate("Classic double-6", numberPlayers, 6, strategies, teams);
        ITemplate crazyToken = new CrazyTokenTemplate("Crazy Token", numberPlayers, 12, strategies, teams);
        // ...

        List<ITemplate> tSelectionables = new List<ITemplate>(){ classic_9, classic_6, crazyToken, custom };

        SingleSelectionMenu<ITemplate> menu = new SingleSelectionMenu<ITemplate>(tSelectionables, "CHOOSE A TEMPLATE");
        menu.Show();

        return menu.Selected;
    }

    public static int MaxTokenMenu(int baseMaxToken)
    {
        List<SimpleOption> maxTokensList = new List<SimpleOption>();

        // default desde doble 6 hasta 12
        for (int i = baseMaxToken; i < 13; i++)
        {
            string optionName = $"Double - {i}";
            maxTokensList.Add(new SimpleOption(optionName));
        }

        SingleSelectionMenu<SimpleOption> menu = new SingleSelectionMenu<SimpleOption>(maxTokensList, "CHOOSE MAX DOUBLE");
        menu.Show();

        return menu.SelectedIndex + baseMaxToken;
    }

    public static WinBoard OverConditionMenu()
    {
        // Options
        GenericOption<WinBoard> classicWinBoard = new GenericOption<WinBoard>(BoardWins.ClassicWinBoard, "Classic");
        GenericOption<WinBoard> crazyTokenWinBoard = new GenericOption<WinBoard>(BoardWins.CrazyTokenWinBoard, "Crazy Token");
        // ...
        
        List<GenericOption<WinBoard>> boardWins = new List<GenericOption<WinBoard>>(){ classicWinBoard, crazyTokenWinBoard };
        SingleSelectionMenu<GenericOption<WinBoard>> menu = new SingleSelectionMenu<GenericOption<WinBoard>>(boardWins, "CHOOSE CONDITION TO OVER A ROUND");
        menu.Show();

        return menu.Selected.Option;
    }

    public static WinnerBoard GetWinnerMenu()
    {
        // Options
        GenericOption<WinnerBoard> classicGetWinner = new GenericOption<WinnerBoard>(BoardWinners.ClassicGetWinner, "Classic");
        // ...

        List<GenericOption<WinnerBoard>> boardWinnerGetters = new List<GenericOption<WinnerBoard>>(){ classicGetWinner };
        SingleSelectionMenu<GenericOption<WinnerBoard>> winnerGetterMenu = new SingleSelectionMenu<GenericOption<WinnerBoard>>(boardWinnerGetters, "CHOOSE JUDGEMENT TO GET THE ROUND WINNER");
        winnerGetterMenu.Show();

        return winnerGetterMenu.Selected.Option;
    }

    public static bool MakeSureCostumizationMenu()
    {
        // Options
        SimpleOption turnBack = new SimpleOption("No, customize again.");
        SimpleOption agreeCustomize = new SimpleOption("Yes, I agree.");
        // ...

        List<SimpleOption> optionsList = new List<SimpleOption>(){ turnBack, agreeCustomize };
        SingleSelectionMenu<SimpleOption> menu = new SingleSelectionMenu<SimpleOption>(optionsList, "DO YOU ACCEPT THIS CUSTOMIZATION");
        menu.Show();

        return menu.Selected.Equals(agreeCustomize);
    }

    public static CircularList<IPlayer> CustomizePlayersMenu(CircularList<IPlayer> currentPlayers, List<IStrategy> strategies, int numberPlayers)
    {
        List<IPlayer> players = currentPlayers.ToArray().ToList();
        List<GenericOption<IStrategy>> strategyOptions = new List<GenericOption<IStrategy>>();

        for (int i = 0; i < strategies.Count; i++)
        {
            strategyOptions.Add(new GenericOption<IStrategy>(strategies[i], strategies[i].ToString()!));
        }
        for (int i = 0; i < numberPlayers; i++)
        {
            SingleSelectionMenu<GenericOption<IStrategy>> menuPlayer = new SingleSelectionMenu<GenericOption<IStrategy>>(strategyOptions, $"Customize {players[i]}");
            menuPlayer.Show();

            players[i] = (new Player(players[i].Name, new List<IStrategy>(){ strategies[menuPlayer.SelectedIndex] }));
        }

        return TemplateUtils.ToCircularList(players);
    }


}
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

    public static ITemplate TemplateMenu(int numberPlayers, List<IStrategy> strategies, ITemplate custom)
    {
        // Options
        ITemplate classic_9 = new ClassicTemplate("Classic double-9", numberPlayers, 9, strategies);
        ITemplate classic_6 = new ClassicTemplate("Classic double-6", numberPlayers, 6, strategies);
        ITemplate crazyToken = new CrazyTokenTemplate("Crazy Token", numberPlayers, 9, strategies);
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
        DelegateOption<WinBoard> classicWinBoard = new DelegateOption<WinBoard>(BoardWins.ClassicWinBoard, "Classic");
        DelegateOption<WinBoard> crazyTokenWinBoard = new DelegateOption<WinBoard>(BoardWins.CrazyTokenWinBoard, "Crazy Token");
        // ...
        
        List<DelegateOption<WinBoard>> boardWins = new List<DelegateOption<WinBoard>>(){ classicWinBoard, crazyTokenWinBoard };
        SingleSelectionMenu<DelegateOption<WinBoard>> menu = new SingleSelectionMenu<DelegateOption<WinBoard>>(boardWins, "CHOOSE CONDITION TO OVER A ROUND");
        menu.Show();

        return menu.Selected.Deleg;
    }

    public static WinnerBoard GetWinnerMenu()
    {
        // Options
        DelegateOption<WinnerBoard> classicGetWinner = new DelegateOption<WinnerBoard>(BoardWinners.ClassicGetWinner, "Classic");
        // ...

        List<DelegateOption<WinnerBoard>> boardWinnerGetters = new List<DelegateOption<WinnerBoard>>(){ classicGetWinner };
        SingleSelectionMenu<DelegateOption<WinnerBoard>> winnerGetterMenu = new SingleSelectionMenu<DelegateOption<WinnerBoard>>(boardWinnerGetters, "CHOOSE JUDGEMENT TO GET THE ROUND WINNER");
        winnerGetterMenu.Show();

        return winnerGetterMenu.Selected.Deleg;
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
}
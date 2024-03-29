using Utils;
using DominoLibrary;

namespace ConsoleApp;

public static partial class Menus
{
    public static bool HumanPlayMenu()
    {
        // Options
        SimpleOption humanOption = new SimpleOption("I will play");
        SimpleOption pcOption = new SimpleOption("Only PC will play");
        List<SimpleOption> hpSelectionables = new List<SimpleOption>(){ humanOption, pcOption };
        // ...

        SingleSelectionMenu<SimpleOption> menu = new SingleSelectionMenu<SimpleOption>(hpSelectionables, "WHAT DO YOU LIKE TO DO", false);
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
        SingleSelectionMenu<SimpleOption> menu = new SingleSelectionMenu<SimpleOption>(tbSelectionables, "SELECT A GAME MODE", false);
        menu.Show();

        return menu.Selected.Equals(boardOption);
    }

    public static ITemplate TemplateMenu(List<IStrategy> strategies, ITemplate custom, bool humanPlay, HumanPlayerMenu humanMenu)
    {
        List<Team> teams = new();
        // Options
        ITemplate classic_9 = new ClassicTemplate("Classic double-9 (Teams)", 4, 9, strategies, teams, false, humanPlay, humanMenu);
        ITemplate classic_9noTeams = new ClassicTemplate("Classic double-9 (Single Player)", 4, 9, strategies, teams, true, humanPlay, humanMenu);
        ITemplate classic_6 = new ClassicTemplate("Classic double-6", 4, 6, strategies, teams, false, humanPlay, humanMenu);
        ITemplate crazyToken = new CrazyTokenTemplate("Crazy Token", 6, 12, strategies, teams, false, humanPlay, humanMenu);
        // ...

        List<ITemplate> tSelectionables = new List<ITemplate>(){ classic_9, classic_9noTeams, classic_6, crazyToken, custom };

        SingleSelectionMenu<ITemplate> menu = new SingleSelectionMenu<ITemplate>(tSelectionables, "CHOOSE A TEMPLATE OR CUSTOMIZE ONE", false);
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

        SingleSelectionMenu<SimpleOption> menu = new SingleSelectionMenu<SimpleOption>(maxTokensList, "CHOOSE MAX DOUBLE", false);
        menu.Show();

        return menu.SelectedIndex + baseMaxToken;
    }

    public static HandOut HandOutMenu()
    {
        // Options
        GenericOption<HandOut> randomHandOut = new GenericOption<HandOut>(HandOuts.Random_HandOut, "Random");
        GenericOption<HandOut> biggerFirst = new GenericOption<HandOut>(HandOuts.BiggerFirstUnfair, "Bigger First and Unfair");
        // ...

        List<GenericOption<HandOut>> options = new List<GenericOption<HandOut>>(){ randomHandOut, biggerFirst };
        SingleSelectionMenu<GenericOption<HandOut>> menu = new SingleSelectionMenu<GenericOption<HandOut>>(options, "CHOOSE HAND OUT JUDGMENT", false);
        menu.Show();

        return menu.Selected.Value;
    }

    public static OverRound OverConditionMenu()
    {
        // Options
        GenericOption<OverRound> classicWinBoard = new GenericOption<OverRound>(RoundOvers.ClassicOverRound, "Classic");
        GenericOption<OverRound> crazyTokenWinBoard = new GenericOption<OverRound>(RoundOvers.CrazyTokenOverRound, "Crazy Token");
        // ...
        
        List<GenericOption<OverRound>> boardWins = new List<GenericOption<OverRound>>(){ classicWinBoard, crazyTokenWinBoard };
        SingleSelectionMenu<GenericOption<OverRound>> menu = new SingleSelectionMenu<GenericOption<OverRound>>(boardWins, "CHOOSE CONDITION TO OVER A ROUND", false);
        menu.Show();

        return menu.Selected.Value;
    }

    public static WinnerRoundGetter GetWinnerMenu()
    {
        // Options
        GenericOption<WinnerRoundGetter> classicGetWinner = new GenericOption<WinnerRoundGetter>(RoundWinners.ClassicGetWinner, "Classic");
        GenericOption<WinnerRoundGetter> randomGetWinner = new GenericOption<WinnerRoundGetter>(RoundWinners.GetRandomWinner, "Random");
        GenericOption<WinnerRoundGetter> smallest5Multiple = new GenericOption<WinnerRoundGetter>(RoundWinners.Smallest5MultipleGetWinner, "Smallest 5 Multiple");
        // ...

        List<GenericOption<WinnerRoundGetter>> boardWinnerGetters = new List<GenericOption<WinnerRoundGetter>>(){ classicGetWinner, randomGetWinner, smallest5Multiple };
        SingleSelectionMenu<GenericOption<WinnerRoundGetter>> menu = new SingleSelectionMenu<GenericOption<WinnerRoundGetter>>(boardWinnerGetters, "CHOOSE JUDGEMENT TO GET THE ROUND WINNER", false);
        menu.Show();

        return menu.Selected.Value;
    }

    public static PointsGetter GetWinnerPoints()
    {
        // Options
        GenericOption<PointsGetter> classicPointsGetter = new GenericOption<PointsGetter>(PointsWinner.ClassicGetPoints, "Classic");
        GenericOption<PointsGetter> get5MultiplesPoints = new GenericOption<PointsGetter>(PointsWinner.Get5MultiplesPoints, "5 Multiples");
        // ...

        List<GenericOption<PointsGetter>> boardWinnerGetters = new List<GenericOption<PointsGetter>>(){ classicPointsGetter, get5MultiplesPoints };
        SingleSelectionMenu<GenericOption<PointsGetter>> menu = new SingleSelectionMenu<GenericOption<PointsGetter>>(boardWinnerGetters, "CHOOSE JUDGEMENT TO GET THE WINNER'S POINTS", false);
        menu.Show();

        return menu.Selected.Value;
    }

    public static bool MakeSureCostumizationMenu()
    {
        // Options
        SimpleOption turnBack = new SimpleOption("No, customize again.");
        SimpleOption agreeCustomize = new SimpleOption("Yes, I agree.");
        // ...

        List<SimpleOption> optionsList = new List<SimpleOption>(){ turnBack, agreeCustomize };
        SingleSelectionMenu<SimpleOption> menu = new SingleSelectionMenu<SimpleOption>(optionsList, "DO YOU ACCEPT THIS CUSTOMIZATION", false);
        menu.Show();

        return menu.Selected.Equals(agreeCustomize);
    }

    public static List<IPlayer> CustomizePlayersMenu(List<IPlayer> currentPlayers, List<IStrategy> strategies, int numberPlayers)
    {
        List<IPlayer> players = currentPlayers;
        List<GenericOption<IStrategy>> strategyOptions = new List<GenericOption<IStrategy>>();

        for (int i = 0; i < strategies.Count; i++)
        {
            strategyOptions.Add(new GenericOption<IStrategy>(strategies[i], strategies[i].ToString()!));
        }
        for (int i = 0; i < numberPlayers; i++)
        {
            SingleSelectionMenu<GenericOption<IStrategy>> menuPlayer = new SingleSelectionMenu<GenericOption<IStrategy>>(strategyOptions, $"Customize {players[i]}", false);
            menuPlayer.Show();

            players[i] = (new SingleStrategyPlayer(players[i].Name, strategies[menuPlayer.SelectedIndex]));
        }

        return players;
    }

    public static int NumberPlayersMenu()
    {
        int min = 2;
        int max = 6;

        List<GenericOption<int>> options = new List<GenericOption<int>>();
        for (int i = min; i <= max; i++)
        {
            options.Add(new GenericOption<int>(i, $"{i} Players"));
        }

        SingleSelectionMenu<GenericOption<int>> menu = new SingleSelectionMenu<GenericOption<int>>(options, "HOW MANY PLAYERS WILL PLAY", false);
        menu.Show();
        
        return menu.Selected.Value;
    }

    public static InnerGetter InnerSelectorMenu()
    {
        // Options
        GenericOption<InnerGetter> randomGetInner = new GenericOption<InnerGetter>(InnerPlayer.Random_Inner, "Random");
        GenericOption<InnerGetter> biggerToken = new GenericOption<InnerGetter>(InnerPlayer.Bigger_Token, "Bigger Token");
        GenericOption<InnerGetter> minDouble = new GenericOption<InnerGetter>(InnerPlayer.Min_Double, "Min Double");
        GenericOption<InnerGetter> maxData = new GenericOption<InnerGetter>(InnerPlayer.Max_Data, "Max Data");
        // ...

        List<GenericOption<InnerGetter>> innerOptions = new List<GenericOption<InnerGetter>>(){ randomGetInner, biggerToken, minDouble, maxData };
        SingleSelectionMenu<GenericOption<InnerGetter>> menu = new SingleSelectionMenu<GenericOption<InnerGetter>>(innerOptions, "CHOOSE JUDGEMENT TO GET INNER", false);
        menu.Show();

        return menu.Selected.Value;
    }
}
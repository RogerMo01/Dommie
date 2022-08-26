using Utils;
using DominoLibrary;

namespace ConsoleApp;

public class CustomizeGame
{
    List<IStrategy> Strategies;
    bool HumanPlay;
    bool SinglePlayerGame = true;
    int BaseMaxToken = 6;
    int MaxToken = 6;
    int ScoreTournamentWin = 100;
    int NumberPlayers = 4;
    List<Team> Teams;
    List<IPlayer> Players;
    OverRound OverRoundCondition = RoundOvers.ClassicOverRound;
    WinnerRoundGetter GetWinnerJudgment = RoundWinners.ClassicGetWinner;
    PointsGetter GetWinnerPoints = PointsWinner.ClassicGetPoints;
    HandOut HandOutJudgment = HandOuts.Random_HandOut;
    InnerGetter InnerSelector = InnerPlayer.Random_Inner;
    HumanPlayerMenu HumanMenu;

    public CustomizeGame(List<IStrategy> strategies, bool humanPlay, HumanPlayerMenu humanMenu)
    {
        Strategies = strategies;
        HumanPlay = humanPlay;
        Players = TemplateUtils.GeneratePlayers(NumberPlayers, Strategies);
        HumanMenu = humanMenu;

        if(HumanPlay)
        {
            Random r = new();
            Players[r.Next(NumberPlayers)] = new HumanPlayer(Strategies[0]);
        }

        Teams = Menus.GenerateUnitaryTeams(Players, HumanPlay);
    }

    public ITemplate Start(ref bool JustRoundGame)
    {
        SimpleOption justRoundMenu = new SimpleOption("Game Mode");
        SimpleOption playersMenu = new SimpleOption("Players");
        SimpleOption teamsMenu = new SimpleOption("Teams");
        SimpleOption maxTokenMenu = new SimpleOption("Max Token");
        SimpleOption handOutMenu = new SimpleOption("Hand Out Judgment");
        SimpleOption innerSelector = new SimpleOption("Inner Selector");
        SimpleOption overRoundMenu = new SimpleOption("Over Round Condition");
        SimpleOption getWinnerMenu = new SimpleOption("Winner getter Judgment");
        SimpleOption getPointsMenu = new SimpleOption("Points getter Judgment");
        SimpleOption scoreMenu = new SimpleOption("Set Tournament Win Score");

        List<SimpleOption> options = new(){ justRoundMenu, playersMenu, teamsMenu, maxTokenMenu, handOutMenu, innerSelector, overRoundMenu, getWinnerMenu, getPointsMenu, scoreMenu };

        SingleSelectionMenu<SimpleOption> menu = new(options, "CUSTOMIZE THE GAME", true);
        
        do
        {
            menu.Show();            

            switch (menu.SelectedIndex)
            {
                case 0:
                JustRoundGame = Menus.GameModeMenu();
                break;

                case 1: //Players
                CustomizePlayers();
                if(SinglePlayerGame)
                {
                    Teams = Menus.GenerateUnitaryTeams(Players, HumanPlay);
                }
                else
                {
                    Teams = TemplateUtils.AssignTeamsClassic(Players);
                }
                break;

                case 2:
                CustomizeTeams();
                break;

                case 3:
                MaxToken = Menus.MaxTokenMenu(BaseMaxToken);
                break;

                case 4:
                HandOutJudgment = Menus.HandOutMenu();
                break;

                case 5:
                InnerSelector = Menus.InnerSelectorMenu();
                break;

                case 6:
                OverRoundCondition = Menus.OverConditionMenu();
                break;

                case 7:
                GetWinnerJudgment = Menus.GetWinnerMenu();
                break;

                case 8:
                GetWinnerPoints = Menus.GetWinnerPoints();
                break;

                case 9:
                if(!JustRoundGame)
                {
                    WriteMenu scoreWriteMenu = new WriteMenu("DECIDE AND WRITE THE NEEDED POINTS TO WIN THE TOURNAMENT");
                    scoreWriteMenu.Show();
                    ScoreTournamentWin = scoreWriteMenu.Selected;
                }
                else
                {
                    QuickScreen screen = new QuickScreen("No allowed in Single Play", 3);
                    screen.Show();
                }
                break;

                default:
                break;
            }

        } while (menu.Selected != null);      
        

        return TemplateUtils.BuildTemplate(TemplateUtils.ToCircularList(Players), MaxToken, NumberPlayers, ScoreTournamentWin, OverRoundCondition, GetWinnerJudgment, GetWinnerPoints, InnerSelector, Teams, HumanPlay, HandOutJudgment, HumanMenu);
    }

    public void CustomizePlayers()
    {
        // NUMBER PLAYERS MENU ~~~~~~
        NumberPlayers = Menus.NumberPlayersMenu();
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~

        // Generar players
        Players = TemplateUtils.GeneratePlayers(NumberPlayers, Strategies);
        if(HumanPlay)
        {
            Random r = new();
            Players[r.Next(NumberPlayers)] = new HumanPlayer(Strategies[0]);
        }
        
        if(!HumanPlay)
        {
            // PLAYERS MENU ~~~~~~
            Players = Menus.CustomizePlayersMenu(Players, Strategies, NumberPlayers);
            // ~~~~~~~~~~~~~~~~~~~
        }
    }
    public void CustomizeTeams()
    {
        SinglePlayerGame = Menus.SinglePlayerOrTeamMenu();

        if (!SinglePlayerGame)
        {
            // CUSTOM TEAM MENU ~~~~~
            Teams = Menus.AssignTeamsMenu(Players, SinglePlayerGame, HumanPlay);
            // ~~~~~~~~~~~~~~~~~~~~~~
        }
    }
}
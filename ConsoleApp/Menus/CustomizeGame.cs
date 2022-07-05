using Utils;
using DominoLibrary;

namespace ConsoleApp;

public class CustomizeGame
{

    List<IStrategy> Strategies;
    bool HumanPlay;
    bool SinglePlayerGame;
    bool JustBoardGame;
    int BaseMaxToken = 6;
    int MaxToken = 6;
    int ScoreTournamentWin = 100;
    int NumberPlayers = 4;
    List<Team> Teams;
    List<IPlayer> Players;
    WinBoard OverBoardCondition = BoardWins.ClassicWinBoard;
    WinnerBoard GetWinnerJudgment = BoardWinners.ClassicGetWinner;

    public CustomizeGame(List<IStrategy> strategies, bool humanPlay, bool justBoard, bool singlePlayer, List<Team> teams)
    {
        Strategies = strategies;
        HumanPlay = humanPlay;
        JustBoardGame = justBoard;
        SinglePlayerGame = singlePlayer;
        Players = TemplateUtils.GeneratePlayers(NumberPlayers, Strategies);

        if(SinglePlayerGame)
        {
            Teams = Menus.GenerateUnitaryTeams(Players, HumanPlay);
        }
        else
        {
            Teams = TemplateUtils.AssignTeamsClassic(Players);
        }
    }

    public ITemplate Start()
    {
        SimpleOption playersMenu = new SimpleOption("Players");
        SimpleOption teamsMenu = new SimpleOption("Teams");
        SimpleOption maxTokenMenu = new SimpleOption("Max Token");
        SimpleOption overBoardMenu = new SimpleOption("Over Board Condition");
        SimpleOption getWinnerMenu = new SimpleOption("Get Winner Judgment");
        SimpleOption scoreMenu = new SimpleOption("Set Tournament Win Score");

        List<SimpleOption> options = new(){ playersMenu, teamsMenu, maxTokenMenu, overBoardMenu, getWinnerMenu, scoreMenu };

        SingleSelectionMenu<SimpleOption> menu = new(options, "CUSTOMIZE THE GAME", true);
        
        do
        {
            menu.Show();            

            switch (menu.SelectedIndex)
            {
                case 0: //Players
                CustomizePlayers();
                break;

                case 1:
                CustomizeTeams();
                break;

                case 2:
                MaxToken = Menus.MaxTokenMenu(BaseMaxToken);
                break;

                case 3:
                OverBoardCondition = Menus.OverConditionMenu();
                break;

                case 4:
                GetWinnerJudgment = Menus.GetWinnerMenu();
                break;

                case 5:
                if(!JustBoardGame)
                {
                    WriteMenu scoreWriteMenu = new WriteMenu("DECIDE AND WRITE THE NEEDED POINTS TO WIN THE TOURNAMENT");
                    scoreWriteMenu.Show();
                    ScoreTournamentWin = scoreWriteMenu.Selected;
                }
                break;

                case -1: // Continue Option
                break;

                default:
                break;
            }

        } while (menu.Selected != null);      
        

        return TemplateUtils.BuildTemplate(TemplateUtils.ToCircularList(Players), MaxToken, NumberPlayers, ScoreTournamentWin, OverBoardCondition, GetWinnerJudgment, Teams);
    }

    private void CustomizePlayers()
    {
        // NUMBER PLAYERS MENU ~~~~~~
        NumberPlayers = Menus.NumberPlayersMenu();
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~

        // Generar players
        Players = TemplateUtils.GeneratePlayers(NumberPlayers, Strategies);
        if(HumanPlay){} // poner al jugador humano de primero en la lista !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        
        if(!HumanPlay)
        {
            // PLAYERS MENU ~~~~~~
            Players = Menus.CustomizePlayersMenu(Players, Strategies, NumberPlayers);
            // ~~~~~~~~~~~~~~~~~~~
        }
    }
    private void CustomizeTeams()
    {
        SinglePlayerGame = Menus.SinglePlayerOrTeamMenu();

        if (!SinglePlayerGame)
        {
            // CUSTOM TEAM MENU !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Teams = Menus.AssignTeamsMenu(Players, SinglePlayerGame, HumanPlay);
            // ~~~~~~~~~~~~~~~~~~~~~~
        }
        
    }
}
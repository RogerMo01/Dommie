using DominoLibrary;
using Utils;
namespace ConsoleApp;

public class ClassicTemplate : ITemplate
{
    public Board Board { get; private set; }
    public Tournament Tournament { get; private set; }
    public string Title { get; private set; }

    public ClassicTemplate(string name, int numberPlayers, int maxToken, List<IStrategy> strategies, List<Team> teams, bool singlePlayer, bool humanPlay)
    {
        Title = name;
        
        // Players
        List<IPlayer> listPlayers = TemplateUtils.GeneratePlayers(4, strategies);
        if(humanPlay) // set first player as human
        {
            Random rand = new();
            listPlayers[rand.Next(4)] = new HumanPlayer(strategies[0], ConsoleColor.White);
        }
        CircularList<IPlayer> players = TemplateUtils.ToCircularList(listPlayers);

        // Teams
        if(singlePlayer)
        {
            teams = Menus.GenerateUnitaryTeams(players.ToArray().ToList(), false);
        }
        else
        {
            teams = TemplateUtils.AssignTeamsClassic(players.ToArray().ToList());
        }

        // Judge
        OverBoard winB = BoardOvers.ClassicOverBoard;
        WinnerBoard winnerB = BoardWinners.ClassicGetWinner;
        PointsGetter pointsGetter = PointsWinner.ClassicGetPoints;
        Inner innerSelector = InnerPlayer.Random_Inner;
        
        Judge judge = new Judge(winB, winnerB, pointsGetter, innerSelector);

        // Inner
        Random r = new Random();
        int inner = r.Next(4);

        // HandOut
        HandOut handOut = HandOuts.Random_HandOut;

        Tournament = new Tournament(new TournamentSetting(players, maxToken, numberPlayers, handOut, 100, judge, teams, humanPlay));
        Board = new Board(new BoardSetting(players, players.First, Tournament.GameTokens, Tournament.TokensPerPlayer, handOut, judge, teams, humanPlay));
    }

    public override string ToString() => this.Title;
}
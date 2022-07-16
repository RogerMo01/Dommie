using DominoLibrary;
using Utils;
namespace ConsoleApp;

public class CrazyTokenTemplate : ITemplate
{
    public Board Board { get; private set; }
    public Tournament Tournament { get; private set; }
    public string Title { get; private set; }

    public CrazyTokenTemplate(string name, int numberPlayers, int maxToken, List<IStrategy> strategies, List<Team> teams, bool singlePlayer, bool humanPlay)
    {
        Title = name;

        // Players
        List<IPlayer> listPlayers = TemplateUtils.GeneratePlayers(6, strategies);
        if(humanPlay) // set first player as human
        {
            Random rand = new();
            listPlayers[rand.Next(5)] = new HumanPlayer(strategies[0], ConsoleColor.White);
        }
        CircularList<IPlayer> players = TemplateUtils.ToCircularList(listPlayers);

        // Teams        
        teams = TemplateUtils.AssignTeamsClassic(players.ToArray().ToList());

        // Judge
        OverBoard winB = BoardOvers.CrazyTokenWinBoard;
        WinnerBoard winnerB = BoardWinners.ClassicGetWinner;
        PointsGetter pointsGetter = PointsWinner.ClassicGetPoints;
        Judge judge = new Judge(winB, winnerB, pointsGetter);

        // Inner
        Random r = new Random();
        int inner = r.Next(4);

        // HandOut
        HandOut handOut = HandOuts.Random_HandOut;

        Tournament = new Tournament(new TournamentSetting(players, maxToken, numberPlayers, handOut, 600, judge, teams, humanPlay));
        Board = new Board(new BoardSetting(players, players.First, Tournament.GameTokens, Tournament.TokensPerPlayer, handOut, judge, teams, humanPlay));
    }

    public override string ToString() => this.Title;
}
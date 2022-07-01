using DominoLibrary;
using Utils;
namespace ConsoleApp;


class CrazyTokenTemplate : ITemplate
{
    public Board Board { get; private set; }
    public Tournament Tournament { get; private set; }
    public string Title { get; private set; }

    public CrazyTokenTemplate(string name, int numberPlayers, int maxToken, List<IStrategy> strategies, List<Team> teams)
    {
        Title = name;

        // Players
        CircularList<IPlayer> players = TemplateUtils.GeneratePlayers(6, strategies);

        // Teams
        teams = TemplateUtils.AsignTeamsClassic(players.ToArray().ToList());

        // Judge
        WinBoard winB = BoardWins.CrazyTokenWinBoard;
        WinnerBoard winnerB = BoardWinners.ClassicGetWinner;
        Judge judge = new Judge(winB, winnerB);

        // Inner
        Random r = new Random();
        int inner = r.Next(4);

        Tournament = new Tournament(new TournamentSetting(players, maxToken, numberPlayers, 300, judge, teams));
        Board = new Board(new BoardSetting(players, players.First, Tournament.GameTokens, Tournament.TokensPerPlayer, judge, teams));
    }

    public override string ToString() => this.Title;
}
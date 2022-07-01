using DominoLibrary;
using Utils;
namespace ConsoleApp;

class ClassicTemplate : ITemplate
{
    public Board Board { get; private set; }
    public Tournament Tournament { get; private set; }
    public string Title { get; private set; }

    public ClassicTemplate(string name, int numberPlayers, int maxToken, List<IStrategy> strategies)
    {
        Title = name;
        
        // Players
        CircularList<IPlayer> players = TemplateUtils.GeneratePlayers(4, strategies);

        // Judge
        WinBoard winB = BoardWins.ClassicWinBoard;
        WinnerBoard winnerB = BoardWinners.ClassicGetWinner;
        Judge judge = new Judge(winB, winnerB);

        // Inner
        Random r = new Random();
        int inner = r.Next(4);

        Tournament = new Tournament(new TournamentSetting(players, maxToken, 100, judge));
        Board = new Board(new BoardSetting(players, players.First, Tournament.GameTokens, Tournament.TokensPerPlayer, judge));
    }

    public override string ToString() => this.Title;
}
using DominoLibrary;
using Utils;
namespace ConsoleApp;

public class ClassicTemplate : ITemplate
{
    public Round Round { get; private set; }
    public Tournament Tournament { get; private set; }
    public string Title { get; private set; }

    public ClassicTemplate(string name, int numberPlayers, int maxToken, List<IStrategy> strategies, List<Team> teams, bool singlePlayer, bool humanPlay, HumanPlayerMenu humanMenu)
    {
        Title = name;
        
        // Players
        List<IPlayer> listPlayers = TemplateUtils.GeneratePlayers(4, strategies);
        if(humanPlay) // set first player as human
        {
            Random rand = new();
            listPlayers[rand.Next(4)] = new HumanPlayer(strategies[0]);
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
        OverRound winB = RoundOvers.ClassicOverRound;
        WinnerRoundGetter winnerB = RoundWinners.ClassicGetWinner;
        PointsGetter pointsGetter = PointsWinner.ClassicGetPoints;
        InnerGetter innerSelector = InnerPlayer.Random_Inner;
        HandOut handOut = HandOuts.Random_HandOut;
        
        Judge judge = new Judge(winB, winnerB, pointsGetter, innerSelector, handOut);

        // Game tokens
        List<Token> gameTokens = Utils.Utils.GenerateTokens(maxToken);

        Tournament = new Tournament(new TournamentSetting(players, maxToken, numberPlayers, 100, judge, teams, humanMenu));
        Round = new Round(new RoundSetting(players, gameTokens, Utils.Utils.DecideTokensPerPlayer(gameTokens.Count, players.Count), judge, teams, maxToken, humanMenu));
    }

    public override string ToString() => this.Title;
}
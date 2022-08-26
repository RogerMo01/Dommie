using Utils;
namespace DominoLibrary;

public class Round : IGame
{
    public CircularList<IPlayer> Players {get; private set;} 
    Token CrazyToken;
    GameStatus Status;
    Judge Judge;
    HumanPlayerMenu HumanMenu;

    public Round(RoundSetting setting)
    {
        Players = setting.Players!;
        Judge = setting.Judge!;
        
        Dictionary<IPlayer, List<Token>> playersTokens = Judge.HandOutJudgment!(setting.GameTokens, Players, setting.TokensPerPlayer);
        CrazyToken = Utils.Utils.RandomTokenGenerator(Utils.Utils.GetAllTokens(playersTokens));

        Status = new GameStatus(setting.Teams!, Players, setting.MaxToken, new GameStatus(0, new(), null!), setting.Judge!, playersTokens);

        // this sort the list starting now by the first player should play
        Players = Players.RotateTill(setting.Judge!.InnerSelector(playersTokens));

        HumanMenu = setting.HumanMenu!;
    }
    
    public IEnumerable<GameStatus> NextMove()
    {
        yield return Status.Clone();

        foreach (var player in Players)
        {
            IPlay play = GetPlay(player);

            Status = Status.UpdateRoundStatus(play, false, null!);

            // Checks if Board is Over with last play, sending copies of parameters
            if(Judge.OverBoard( Status.Clone(), Status.PlayersTokens!.ToDictionary(x => x.Key, x => x.Value), CrazyToken.Clone()))
            {
                // gets the winner of the round
                (Team players , int score) winner = Judge.WinnerBoard(Status.Clone(), Judge.WinnerPointsGetter, Status.PlayersTokens!.ToDictionary(x => x.Key, x => x.Value));

                Status = Status.UpdateRoundStatus(play, true, new WinnerStatus(winner.players, winner.score));
                yield return Status.Clone();
                break;
            }
            
            yield return Status.Clone();
        }
    }

    private IPlay GetPlay(IPlayer player)
    {
        int wrongPlays = 0;

        // default is pass, if it's a play, will be substituted
        IPlay play = new Pass(player);

        // only if current player have token to play or it's initial play
        if((HaveToken(player)) || Status.BoardTokens!.Count == 0)
        {
            do
            {
                if(wrongPlays >= 3)
                {
                    play = new Pass(player);
                    break;
                }

                play = player.Play(new BoardInfo(Status), Status.PlayersTokens![player].ToList(), HumanMenu);
                wrongPlays++;
            } 
            while (!Judge.IsValid(Status.BoardTokens!.Count, Status.Ends!, play.Clone()) && wrongPlays <= 3);

            wrongPlays = 0;
        }

        return play;
    }

    private bool HaveToken(IPlayer player)
    {    
        List<Token> playerTokens = Status.PlayersTokens![player];

        for (int i = 0; i < Status.Ends!.Length; i++)
        {
            for (int j = 0; j < playerTokens.Count; j++)
            {
                if(playerTokens[j].Right == Status.Ends[i] || playerTokens[j].Left == Status.Ends[i])
                {
                    return true;
                }
            }
        }
        
        return false;
    }
}
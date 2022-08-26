using Utils;
namespace DominoLibrary;

public class Tournament : IGame
{
    List<Token> GameTokens { get; } 
    int TokensPerPlayer;
    TournamentSetting Settings;
    GameStatus Status;

    public Dictionary<Team, int> TeamsScore { get; private set; }

    public Tournament(TournamentSetting setting)
    {
        Settings = setting;
        GameTokens = Utils.Utils.GenerateTokens(setting.MaxToken);
        TokensPerPlayer = Utils.Utils.DecideTokensPerPlayer(GameTokens.Count, setting.Players!.Count);
        
        Status = new GameStatus(setting.WinScore, Utils.Utils.SetTeamsScore(setting.Teams!), null!);
        TeamsScore = Status.TeamsScore;
    }

    public IEnumerable<GameStatus> NextMove()
    {
        while(true)
        {
            // Initilize the Round
            RoundSetting rs = new RoundSetting(Settings.Players!, GameTokens, TokensPerPlayer, Settings.Judge!, Settings.Teams!, Settings.MaxToken, Settings.HumanMenu!);
            Round round = new Round(rs);

            GameStatus lastStatus = Status;
            
            // iterate rounds
            foreach (var gameStatus in round.NextMove())
            {
                lastStatus = gameStatus;
                yield return gameStatus.Clone();
            }

            Status = Status.UpdateTournamentStatus(false, null!, lastStatus);

            if(IsOver())
            {
                (Team players , int score) winner = GetWinner();
                WinnerStatus result = new WinnerStatus(winner.players, winner.score);
                Status = Status.UpdateTournamentStatus(true, result, Status);

                yield return Status.Clone();
                break; 
            }
        }
    }


    private bool IsOver()
    {
        foreach (var team in Settings.Teams!)
        {
            int points = 0;
        
            points += Status.TeamsScore[team];
            
            if(points >= Status.WinScore) return true;
        }

        return false;
    }

    private (Team, int) GetWinner()  
    {   
        Team winner = Status.TeamsScore.First(x => x.Value >= Status.WinScore).Key;
        return (winner, Status.TeamsScore[winner]);
    }
}
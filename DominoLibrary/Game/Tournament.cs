using Utils;
namespace DominoLibrary;

public class Tournament : IGame
{
    public CircularList<IPlayer> Players { get; private set; } 
    public List<Token> GameTokens { get; private set; } 
    public int TokensPerPlayer { get; private set; }
    public Node<IPlayer> Inner;
    GamePrinter? GamePrinter; 
    public Dictionary<Team, int> TeamsScore { get; private set; }
    public int WinScore { get; private set; }
    public Judge Judge { get; private set; }
    public List<Team> Teams { get; private set;}
    bool HumanPlay;

    public Tournament(TournamentSetting setting) 
    {
        GameTokens = GenerateTokens(setting.MaxToken);
        Players = setting.Players!;
        Inner = GetInner();
        TokensPerPlayer = DecideTokensPerPlayer(GameTokens.Count, setting.TotalPlayers);
        Teams = setting.Team!;
        TeamsScore = SetTeamsScores();
        WinScore = setting.WinScore;
        Judge = setting.Judge!;
        HumanPlay = setting.HumanPlay;
    }

    public GameResult Start()
    {
        int roundNumber = 1;

        while(true)
        {
            GamePrinter!.ShowTournamentStatus(roundNumber, TeamsScore);
            if(HumanPlay) { Lapse l = new Lapse(3); }

            // Initilize the Boards
            BoardSetting bs = new BoardSetting(Players, Inner, GameTokens, TokensPerPlayer, Judge, Teams, HumanPlay);
            Board board = new Board(bs);
            board.SetGamePrinter(GamePrinter!);

            GameResult boardResult = board.Start();

            GamePrinter.PrintPoints(boardResult.Score);
            UpdateTournament(boardResult);

            if(IsOver()){ break; }
            
            roundNumber++;
        }

        (Team team, int score) winner = GetWinner();
        GameResult result = new GameResult(winner.team, winner.score);
        GamePrinter!.PrintTournamentWinner(result); //PRINT

        return result;
    }

    public void SetGamePrinter(GamePrinter gamePrinter)
    {
        GamePrinter = gamePrinter;
        gamePrinter.AddTournament(this);
    }

    private Node<IPlayer> GetInner() // default, FiRST
    {
        return Players.First;
    }

    private int DecideTokensPerPlayer(int totalTokens, int totalPlayers) // default, MAX + 1
    {
        return (totalTokens - ((totalTokens * 29) / 100)) / totalPlayers; 
    }

    private Dictionary<Team, int> SetTeamsScores()
    {
        Dictionary<Team, int> result = new Dictionary<Team, int>();

        for(int i = 0; i < Teams.Count; i++)
        {   
            result.Add(Teams[i], 0);
        }

        return result;
    }

    private void UpdateTournament(GameResult gameResult)
    {
        // si no hay ganador no hay q actualizar puntos y sale otro jugador
        try
        {
            Inner = Players.FindNode(gameResult.Winner.PlayersTeam.First());
            TeamsScore[gameResult.Winner] += gameResult.Score;
        }
        catch (NullReferenceException)
        {
            Inner = Inner.Next!;
        }
    }

    private static List<Token> GenerateTokens(int maxToken)
    {
        List<Token> gameTokens = new List<Token>();

        for(int i = -1; i < maxToken; i++)
        {
            for (int j = i + 1; j <= maxToken; j++)
            {
                gameTokens.Add(new Token(i + 1, j));
            }
        }

        return gameTokens;    
    }

    private (Team, int) GetWinner() // defaul por puntos
    {   
        Team winner = TeamsScore.First(x => x.Value >= WinScore).Key;
        return (winner, TeamsScore[winner]);
    }
   
    private bool IsOver() // default por puntos
    {
        foreach (var team in Teams)
        {
            int points = 0;
        
            points += TeamsScore[team];
            
            if(points >= WinScore) return true;
        }

        return false;
    }
}
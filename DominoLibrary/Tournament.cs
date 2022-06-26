using Utils;
namespace DominoLibrary;

public class Tournament : IGame
{
    public CircularList<IPlayer> Players { get; private set; } 
    public List<Token> GameTokens { get; private set; } 
    public int TokensPerPlayer { get; private set; }
    Node<IPlayer> Inner;
    private GamePrinter? GamePrinter; 
    public Dictionary<IPlayer, int> PlayersScore { get; private set; }
    public int WinScore { get; private set; }
    public Judge Judge { get; private set; }

    public Tournament(TournamentSetting setting) 
    {
        GameTokens = GenerateTokens(setting.MaxToken);
        Players = setting.Players;
        Inner = GetInner();
        TokensPerPlayer = DecideTokensPerPlayer(setting.MaxToken);
        PlayersScore = SetPlayerScores();
        WinScore = setting.WinScore;
        Judge = setting.Judge;
    }

    public GameResult Start()
    {
        int roundNumber = 1;

        while(true)
        {
            GamePrinter!.ShowTournamentStatus(roundNumber, PlayersScore); //PRINT

            BoardSetting bs = new BoardSetting(Players, Inner, GameTokens, TokensPerPlayer, Judge);
            Board board = new Board(bs);
            board.SetGamePrinter(GamePrinter!);

            GameResult boardResult = board.Start();

            UpdateTournament(boardResult);

            if(IsOver()) { break; }
            roundNumber++;
        }

        (IPlayer player, int score) winner = GetWinner();
        GameResult result = new GameResult(winner.player, winner.score);
        GamePrinter.PrintTournamentWinner(result); //PRINT

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

    private int DecideTokensPerPlayer(int maxToken) // default, MAX + 1
    {
        return maxToken + 1;
    }

    private Dictionary<IPlayer, int> SetPlayerScores()
    {
        Dictionary<IPlayer, int> result = new Dictionary<IPlayer, int>();

        Node<IPlayer> current = Players.First;
        for(int i = 0; i < Players.Count; i++)
        {
            result.Add(current.Value, 0);
            current = current.Next!;
        }

        return result;
    }

    private void UpdateTournament(GameResult gameResult)
    {
        // si no hay ganador no hay q actualizar puntos y sale otro jugador
        try
        {
            Inner = Players.FindNode(gameResult.Winner);
            PlayersScore[gameResult.Winner] += gameResult.Score;
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

    private (IPlayer, int) GetWinner() // defaul por puntos
    {
        IPlayer winner = PlayersScore.First(x => x.Value >= WinScore).Key;
        int score = PlayersScore[winner];

        return (winner, score);
    }

    private bool IsOver() // default por puntos
    {
        return PlayersScore.Any(x => x.Value >= WinScore);
    }
}
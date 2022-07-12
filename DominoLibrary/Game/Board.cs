using Utils;
namespace DominoLibrary;

public partial class Board : IGame
{
    public CircularList<IPlayer> Players {get; private set;} 
    public List<Token> GameTokens {get; private set;}  
    Dictionary<IPlayer, List<Token>> PlayersTokens;
    public LinkedList<Token_onBoard> BoardTokens {get; private set;}
    public int[] Ends { get; private set;} = {-1, -1};
    BoardSetting Settings;
    public Judge Judge;
    Token CrazyToken;
    GamePrinter? GamePrinter;
    public int ConsecutivePasses { get; private set; }
    public List<(IPlayer player, Token_onBoard token_OnBoard)> Plays { get; private set; } = new();
    public List<Team> Teams { get; private set;}


    public Board(BoardSetting setting)
    {
        Settings = setting;

        Players = setting.Players!;
        GameTokens = setting.GameTokens;
        Judge = setting.Judge!;

        PlayersTokens = setting.HandOut(GameTokens, Players, setting.TokensPerPlayer);
        CrazyToken = GetCrazyToken();

        BoardTokens = new LinkedList<Token_onBoard>();   

        ConsecutivePasses = 0;
        Plays = new List<(IPlayer player, Token_onBoard token_OnBoard)>();

        Teams = setting.Team!;
    }
    private Board(CircularList<IPlayer> players, List<Token> gameTokens, LinkedList<Token_onBoard> boardTokens, int[] ends, Judge judge, List<(IPlayer , Token_onBoard)> plays, List<Team> teams, int consecPass)
    {
        Players = players;
        GameTokens = gameTokens;
        BoardTokens = boardTokens;
        Ends = ends;
        Judge = judge;
        Plays = plays;
        Teams = teams;
        ConsecutivePasses = consecPass;
        PlayersTokens = null!;
        Settings = null!;
        CrazyToken = null!;
    }

    public GameResult Start()
    {
        if(!Settings.HumanPlay)
        {
            GamePrinter!.ShowPlayerTokens(); //Print initial hand out tokens
        }
        
        foreach (var player in Players)
        {
            // default is pass, if it's a play, will be substituted
            Token_onBoard token = new Pass(new Token(-1, -1), true, player, true);
            
            // freeze two seconds per play
            if(Settings.HumanPlay) { Lapse l = new Lapse(2); }

            // only if current player have token to play or it's initial play
            if((HaveToken(player)) || BoardTokens.Count == 0)
            {
                do
                {
                    token = player.Play(this.Clone(), PlayersTokens[player].ToList(), GamePrinter!.HumanPlayerMenu);
                } 
                while (!Judge.IsValid(this.Clone(), token.Clone()));

                ConsecutivePasses = 0;
            }
            else
            {
                ConsecutivePasses ++;
            }
            
            Plays.Add((player, token));

            UpdateBoard(token, player);
            
            GamePrinter!.PrintPlay(); // print play
            
            // Checks if Board is Over with last play, sending copies of parameters
            if(Judge.OverBoard(this.Clone(), PlayersTokens.ToDictionary(x => x.Key, x => x.Value), CrazyToken.Clone())) { break; }
        }

        // gets the winner of the board
        (Team players , int score) winner = Judge.WinnerBoard(this.Clone(), Judge.WinnerPointsGetter, PlayersTokens.ToDictionary(x => x.Key, x => x.Value));
    
        GamePrinter!.PrintBoardWinner(winner.players, winner.score);
        
        if(Settings.HumanPlay) { Lapse l = new Lapse(2); }
        
        return new GameResult(winner.players, winner.score);
    }
    
    private void ResetEnds()
    {       
        Token_onBoard token = BoardTokens.First!.Value; // tener en cuenta que al inicio de la partida la lista siempre sera null

        Ends[0] = (token.Straight) ? token.Left : token.Right;
    
        token = BoardTokens.Last!.Value;

        Ends[1] = (token.Straight) ? token.Right : token.Left;
    }

    public void SetGamePrinter(GamePrinter gp)
    {
        GamePrinter = gp;
        GamePrinter.AddBoard(this, PlayersTokens); // pasa las fichas de los jugadores
    }

    public Board Clone()
    {
        // Players
        IPlayer[] newPlayersArr = Players.ToArray();
        CircularList<IPlayer> newPlayers = new CircularList<IPlayer>(newPlayersArr[0]);
        for (int i = 1; i < newPlayersArr.Length; i++)
        {
            newPlayers.AddLast(newPlayersArr[i]);
        }

        // Game Tokens
        List<Token> newGameTokens = GameTokens.ToList();

        // Board Tokens
        List<Token_onBoard> tempListGameTokens = BoardTokens.ToList();
        LinkedList<Token_onBoard> newBoardTokens = new();
        foreach (var item in tempListGameTokens)
        {
            newBoardTokens.AddLast(item);
        }

        // Ends
        int[] newEnds = Ends.ToArray();

        // Judge
        Judge newJudge = new Judge(new OverBoard(Judge.OverBoard), new WinnerBoard(Judge.WinnerBoard), new PointsGetter(Judge.WinnerPointsGetter));

        // Plays
        List<(IPlayer, Token_onBoard)> newPlays = Plays.ToList();

        // Teams
        List<Team> newTeams = Teams.ToList();

        Board toReturn = new Board(newPlayers, newGameTokens, newBoardTokens, newEnds, newJudge, newPlays, newTeams, ConsecutivePasses);
        
        return toReturn;
    }
}
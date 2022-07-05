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
    public List<(IPlayer player, Token_onBoard token_OnBoard)> Plays { get; private set; } = new List<(IPlayer player, Token_onBoard token_OnBoard)>();
    public List<Team> Teams { get; private set;}


    public Board(BoardSetting setting)
    {
        Settings = setting;

        Players = setting.Players!;
        GameTokens = setting.GameTokens;
        Judge = setting.Judge!;

        PlayersTokens = HandOut(GameTokens, Players, setting.TokensPerPlayer);
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
        GamePrinter!.ShowPlayerTokens(); //PRINT
        
        foreach (var player in Players)
        {
            // default es pase, si juega se sustituye
            Token_onBoard token = new Pass(new Token(-1, -1), true, player, true);

            if((HaveToken(player)) || BoardTokens.Count == 0)
            {
                do
                {
                    token = player.Play(this.Clone(), PlayersTokens[player].ToList());
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
            
            GamePrinter.PrintPlay(); // imprime jugada

            if(Judge.WinBoard(this.Clone(), PlayersTokens.ToDictionary(x => x.Key, x => x.Value), CrazyToken.Clone())) { break; }
        }

        (Team team, int score) winner = Judge.WinnerBoard(this.Clone(), PlayersTokens);

        GamePrinter.PrintBoardWinner(winner.team, winner.score); //PRINT
        return new GameResult(winner.team, winner.score);
    }
    
    private void ResetEnds()
    {       
        Token_onBoard token = BoardTokens.First!.Value; // tener en cuenta que al inicio de la partida la lista siempre sera null

        Ends[0] = (token.Straight) ? token.Left : token.Right;
    
        token = BoardTokens.Last!.Value;

        Ends[1] = (token.Straight) ? token.Right : token.Left;
    }

    public bool Playable(Token token)
    {
        foreach (var item in Ends)
        {
            if(item == token.Left || item == token.Right) return true;
        }

        return false;
    }

    public bool PlayRight(Token token)
    {
        if(Ends[1] == token.Left || Ends[1] == token.Right) return true;
        
        return false;
    }

    public bool Straight(Token token, bool playRight)
    {
        if(playRight && (Ends[1] == token.Left)) { return true; }
        if(!playRight && (Ends[0] == token.Right)) { return true; }

        return false; 
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
        Judge newJudge = new Judge(new OverBoard(Judge.WinBoard), new WinnerBoard(Judge.WinnerBoard));

        // Plays
        List<(IPlayer, Token_onBoard)> newPlays = Plays.ToList();

        // Teams
        List<Team> newTeams = Teams.ToList();

        return new Board(newPlayers, newGameTokens, newBoardTokens, newEnds, newJudge, newPlays, newTeams, ConsecutivePasses);
    }
}
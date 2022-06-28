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
    private GamePrinter? GamePrinter;  
    public int ConsecutivePasses;
    public List<(IPlayer player, Token_onBoard token_OnBoard)> Plays = new List<(IPlayer player, Token_onBoard token_OnBoard)>();


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
                    token = player.Play(this, PlayersTokens[player]);
                } 
                while (!Judge.IsValid(this, token));

                ConsecutivePasses = 0;
            }
            else
            {
                ConsecutivePasses ++;
            }
            
            Plays.Add((player, token));

            UpdateBoard(token, player);
            
            GamePrinter.PrintPlay(); // imprime jugada

            if(Judge.WinBoard.Invoke(this, PlayersTokens, CrazyToken)) { break; }
        }

        (IPlayer player, int score) winner = Judge.WinnerBoard.Invoke(this, PlayersTokens);

        GamePrinter.PrintBoardWinner(winner.player); //PRINT
        return new GameResult(winner.player, winner.score);
    }
    
    public void ResetEnds()
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

}
using Utils;
namespace DominoLibrary;

public partial class Board
{
    public CircularList<IPlayer> Players {get; private set;}
    public List<Token> GameTokens {get; private set;}
    Dictionary<IPlayer, List<Token>> PlayersTokens;
    public LinkedList<Token_onBoard> BoardTokens {get; private set;}
    public int[] Ends { get; private set;} = {-1, -1};
    BoardSetting Settings;
    public Token_onBoard? LastPlayed {get; private set;}
    public IPlayer? LastPlayer {get; private set;}
    public GamePrinter? GamePrinter;

    int ConsecutivePasses;

    public Board(BoardSetting setting)
    {
        Settings = setting;

        Players = setting.Players;
        GameTokens = setting.GameTokens;

        PlayersTokens = HandOut(GameTokens, Players, setting.TokensPerPlayer);

        BoardTokens = new LinkedList<Token_onBoard>();   

        ConsecutivePasses = 0;
    }

    public GameResult Start()
    {
        Node<IPlayer> currentPlayer = Settings.Inner.Previous!;

        while (true)
        {
            currentPlayer = currentPlayer.Next!;
            Token_onBoard token = null!;

            if(HaveToken(currentPlayer.Value, BoardTokens.Count == 0))
            {
                token = currentPlayer.Value.Play(this, PlayersTokens[currentPlayer.Value]);
                ConsecutivePasses = 0;
            }
            else
            {
                ConsecutivePasses ++;
            }
            
            UpdateBoard(token, currentPlayer.Value);

            GamePrinter!.PrintPlay(); // IMPRIME

            if(IsOver()) { break; }
        }

        (IPlayer player, int score) winner = GetWinner();

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
using Utils;
namespace DominoLibrary;

public partial class Board
{
    public CircularList<IPlayer> Players {get; private set;}
    public List<Token> GameTokens {get; private set;}
    Dictionary<IPlayer, List<Token>> PlayersTokens;
    public LinkedList<Token_onBoard> BoardTokens {get; private set;}
    public int[] Ends { get; private set;} = {-1, -1};
    Setting Settings;
    public Token_onBoard? LastPlayed {get; private set;}
    public IPlayer? LastPlayer {get; private set;}

    int ConsecutivePasses;

    public Board(Setting setting)
    {
        Settings = setting;

        Players = setting.Players;
        GameTokens = GenerateTokens(setting.MaxToken);

        int tokensPerPlayer = setting.MaxToken + 1;
        PlayersTokens = HandOut(GameTokens, Players, tokensPerPlayer);
        
        // pasa parámetros al Printer
        Settings.GamePrinter.AddBoard(this, PlayersTokens);

        BoardTokens = new LinkedList<Token_onBoard>();   

        ConsecutivePasses = 0;
    }

    public GameResult Start()
    {
        foreach (var player in Players)
        {
            Token_onBoard token = null!;

            if(HaveToken(player, BoardTokens.Count == 0))
            {
                token = player.Play(this, PlayersTokens[player]);
                ConsecutivePasses = 0;
            }
            else
            {
                ConsecutivePasses ++;
            }
            
            UpdateBoard(token, player);
            
            Settings.GamePrinter.PrintPlay(); // imprime jugada

            if(IsOver()) { break; }
        }

        IPlayer winner = GetWinner();

        return new GameResult(winner);
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

}
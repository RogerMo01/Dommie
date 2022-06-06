using Utils;
namespace DominoLibrary;

public class Board
{
    CircularList<IPlayer> Players;
    List<Token> GameTokens;
    Dictionary<IPlayer, List<Token>> PlayersTokens;
    LinkedList<Token> BoardTokens;
    Setting Settings;

    public Board(Setting setting)
    {
        Settings = setting;

        Players = setting.Players;
        GameTokens = GenerateTokens(setting.MaxToken);
        PlayersTokens = HandOut(GameTokens);
        BoardTokens = new LinkedList<Token>();
    }

    public GameResult Start()
    {
        
    }



    private static List<Token> GenerateTokens(int maxToken)
    {

    }

    private static Dictionary<IPlayer, List<Token>> HandOut(List<Token> tokens)
    {

    }
}
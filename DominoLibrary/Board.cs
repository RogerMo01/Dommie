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
        Node<IPlayer> currentPlayer = Settings.Inner.Previous!;

        while (true)
        {
            currentPlayer = currentPlayer.Next!;

            if(HaveToken(currentPlayer.Value, PlayersTokens))
            {
                BoardInfo info = new BoardInfo(PlayersTokens[currentPlayer.Value], BoardTokens);
                Token token = currentPlayer.Value.Play(info);

                UpdateBoard(token);
            }

            if(Settings.OverCondition.IsOver(BoardTokens)) { break; }
        }

        IPlayer winner = GetWinner();
        return new GameResult(winner);
    }

    private IPlayer GetWinner()
    {
        //valora quien ganó mirando el tablero
    }

    private static bool HaveToken(IPlayer player, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        //recorrer sus fichas y devolver bool
    }

    private void UpdateBoard(Token token)
    {
        // ponerla al tablero
        // quitarsela al jugador
        //
    }

    private static List<Token> GenerateTokens(int maxToken)
    {

    }

    private static Dictionary<IPlayer, List<Token>> HandOut(List<Token> tokens)
    {

    }
}
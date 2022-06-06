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
        PlayersTokens = HandOut(GameTokens, Players);
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

    private static Dictionary<IPlayer, List<Token>> HandOut(List<Token> tokens, CircularList<IPlayer> players)
    {
        Dictionary<IPlayer, List<Token>> result = new Dictionary<IPlayer, List<Token>>();

        List<Token> clonedTokens = tokens.ToList();

        Node<IPlayer> firstPlayer = players.First;

        for (int i = 0; i < players.Count; i++)
        {
            result.Add(firstPlayer.Value, HandOutToPlayer(clonedTokens));
            firstPlayer = firstPlayer.Next!;
        }

        return result;
    }

    private static List<Token> HandOutToPlayer(List<Token> tokens)
    {
        List<Token> playerTokens = new List<Token>();
        Random random = new Random();

        for (int i = 0; i < 10; i++)  // modificar luego en dependencia de la cantidad de fichas que estan en juego
        {
            int index = random.Next(tokens.Count - 1);
            playerTokens.Add(tokens[index]);
            tokens.RemoveAt(index);
        }

        return playerTokens;
    }
    
}
using Utils;
namespace DominoLibrary;

public class Board
{
    public Board(Setting setting)
    {
        
    }

    public GameResult Start()
    {

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
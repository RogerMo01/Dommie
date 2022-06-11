using Utils;
namespace DominoLibrary;

public class Board
{
    CircularList<IPlayer> Players;
    List<Token> GameTokens;
    Dictionary<IPlayer, List<Token>> PlayersTokens;
    LinkedList<Token_onBoard> BoardTokens;
    Setting Settings;

    public Board(Setting setting)
    {
        Settings = setting;

        Players = setting.Players;
        GameTokens = GenerateTokens(setting.MaxToken);

        int tokensPerPlayer = setting.MaxToken + 1;
        PlayersTokens = HandOut(GameTokens, Players, tokensPerPlayer);

        BoardTokens = new LinkedList<Token_onBoard>();   
    }

    public GameResult Start()
    {
        Node<IPlayer> currentPlayer = Settings.Inner.Previous!;

        while (true)
        {
            currentPlayer = currentPlayer.Next!;

            if(HaveToken(currentPlayer.Value))
            {
                BoardInfo info = new BoardInfo(PlayersTokens[currentPlayer.Value], BoardTokens);
                Token_onBoard token = currentPlayer.Value.Play(info);

                UpdateBoard(token, currentPlayer.Value);
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

    private bool HaveToken(IPlayer player)
    {
        //si es la primera jugada siempre lleva

        int[] ends = GetEnds(BoardTokens);
        List<Token> playerTokens = PlayersTokens[player];

        for (int i = 0; i < ends.Length; i++)
        {
            for (int j = 0; j < playerTokens.Count; j++)
            {
                if(playerTokens[j].Right == ends[i] || playerTokens[j].Left == ends[i])
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    private void UpdateBoard(Token_onBoard token, IPlayer player)
    {
        if(token.PlayRight)
        {
            BoardTokens.AddLast(token);
        }
        else
        {
            BoardTokens.AddFirst(token);
        }

        int index = 0;

        for (int i = 0; i < PlayersTokens[player].Count; i++)
        {
            if(PlayersTokens[player].Contains(token))
            {
                index = i;
                break;
            }   
        }
        
        PlayersTokens[player].RemoveAt(index);
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

    private static Dictionary<IPlayer, List<Token>> HandOut(List<Token> tokens, CircularList<IPlayer> players, int tokensPerPlayer)
    {
        Dictionary<IPlayer, List<Token>> result = new Dictionary<IPlayer, List<Token>>();

        List<Token> clonedTokens = tokens.ToList();

        Node<IPlayer> firstPlayer = players.First;

        for (int i = 0; i < players.Count; i++)
        {
            result.Add(firstPlayer.Value, HandOutToPlayer(clonedTokens, tokensPerPlayer));
            firstPlayer = firstPlayer.Next!;
        }

        return result;
    }

    private static List<Token> HandOutToPlayer(List<Token> tokens, int tokensPerPlayer)
    {
        List<Token> playerTokens = new List<Token>();
        Random random = new Random();

        for (int i = 0; i < tokensPerPlayer ; i++)  
        {
            int index = random.Next(tokens.Count - 1);
            playerTokens.Add(tokens[index]);
            tokens.RemoveAt(index);
        }

        return playerTokens;
    }
    
    private static int[] GetEnds(LinkedList<Token_onBoard> boardTokens)
    {
        int[] ends = new int[2];
        Token_onBoard token = boardTokens.First.Value; // tener en cuenta que al inicio de la partida la lista siempre sera null

        ends[0] = (token.Straight) ? token.Left : token.Right;
    
        token = boardTokens.Last.Value;

        ends[1] = (token.Straight) ? token.Right : token.Left;

        return ends; 
    }
    
}
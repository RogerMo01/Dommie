using Utils;
namespace DominoLibrary;

public class BoardInfo
{
    public int[] Ends { get; }
    public LinkedList<Token_onBoard> BoardTokens { get; }
    public List<IPlay> Plays { get; }
    public int MaxDouble { get; }
    public Judge Judge { get; }
    public IPlayer[] Players { get; }
    public Dictionary<IPlayer, int> PlayersTokensLeft { get; }

    public BoardInfo(GameStatus gameStatus)
    {
        Ends = gameStatus.Ends!.ToArray();

        // board tokens
        List<Token_onBoard> boardTokens = gameStatus.BoardTokens!.ToList();
        LinkedList<Token_onBoard> newBoardTokens = new();
        foreach (var item in boardTokens)
        {
            newBoardTokens.AddLast(item);
        }
        BoardTokens = newBoardTokens;

        MaxDouble = gameStatus.MaxDouble;
        Judge = gameStatus.Judge!;

        Players = gameStatus.Players!.ToArray();

        PlayersTokensLeft = GetTokensLeft(gameStatus.PlayersTokens!);

        Plays = gameStatus.Plays!;
    }

    // count how many tokens left in each player's hand
    private Dictionary<IPlayer, int> GetTokensLeft(Dictionary<IPlayer, List<Token>> hands)
    {
        Dictionary<IPlayer, int> playersTokensLeft = new Dictionary<IPlayer, int>();

        foreach (var player in hands)
        {
            playersTokensLeft.Add(player.Key, player.Value.Count);
        }

        return playersTokensLeft;
    } 
}
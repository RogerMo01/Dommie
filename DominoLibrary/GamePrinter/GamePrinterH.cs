using ConsoleApp;
namespace DominoLibrary;

public partial class GamePrinter
{
    public Token_onBoard HumanPlayerMenu(IPlayer player)
    {
        List<Token> tokens = PlayerTokens![player];

        PlaySelectorMenu tokenSelectorMenu = new(tokens, Board!.Clone(), GetTokensLeft());
        tokenSelectorMenu.Show();
        Token selectedToken = tokenSelectorMenu.Selected;

        SimpleOption playRightOption = new SimpleOption("Play by Right");
        SimpleOption playLeftOption = new SimpleOption("Play by Left");
        List<SimpleOption> playSideOptions = new(){ playLeftOption, playRightOption };

        SingleSelectionMenu<SimpleOption> playSidedSelectorMenu = new( playSideOptions, "Select table side", false);
        playSidedSelectorMenu.Show();
        bool playRight = playSidedSelectorMenu.Selected == playRightOption;

        // try it Straight or otherwise
        Token_onBoard play = new Token_onBoard(tokenSelectorMenu.Selected, true, player, playRight);
        if(Board.Judge.IsValid(Board, play))
        {
            return play;
        }
        else { return new Token_onBoard(tokenSelectorMenu.Selected, false, player, playRight); }
    }
    
    private Dictionary<IPlayer, int> GetTokensLeft()
    {
        Dictionary<IPlayer, int> playersTokensLeft = new Dictionary<IPlayer, int>();

        foreach (var player in PlayerTokens!)
        {
            playersTokensLeft.Add(player.Key, player.Value.Count);
        }

        return playersTokensLeft;
    } 
}
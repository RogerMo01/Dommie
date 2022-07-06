using ConsoleApp;
namespace DominoLibrary;

public class HumanPlayer : IPlayer
{
    public string Name { get; } = "You";

    public List<IStrategy> Strategies { get; }

    public ConsoleColor Color { get; }


    public HumanPlayer(List<IStrategy> strategies, ConsoleColor color)
    {
        Strategies = strategies;
        Color = color;
    }

    public Token_onBoard Play(Board board, List<Token> tokens)
    {
        PlaySelectorMenu tokenSelectorMenu = new(tokens, board);
        tokenSelectorMenu.Show();
        Token selectedToken = tokenSelectorMenu.Selected;

        SimpleOption playRightOption = new SimpleOption("Play by Right");
        SimpleOption playLeftOption = new SimpleOption("Play by Left");
        List<SimpleOption> playSideOptions = new(){ playLeftOption, playRightOption };

        SingleSelectionMenu<SimpleOption> playSidedSelectorMenu = new( playSideOptions, "Select table side", false);
        playSidedSelectorMenu.Show();
        bool playRight = playSidedSelectorMenu.Selected == playRightOption;

        Token_onBoard play = new Token_onBoard(tokenSelectorMenu.Selected, board.Straight(selectedToken, playRight), this, playRight);
        return play;
    }

    public override string ToString() => Name;
}
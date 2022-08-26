using Utils;
using DominoLibrary;
namespace ConsoleApp;

class ConsoleApp
{
    List<IStrategy> Strategies = new List<IStrategy>()
    {
        new BotaGorda(),
        new Mosaic(),
        new Random_Strategy()
    };

    bool HumanPlay;
    bool JustRound;
    ITemplate Template;
    IGame Game;
    Dictionary<IPlayer, ConsoleColor>? PlayersColors;

    public ConsoleApp()
    {
        HumanPlay = Menus.HumanPlayMenu();

        JustRound = Menus.GameModeMenu();
        
        // template choosing
        ITemplate custom = new CustomTemplate();
        Template = Menus.TemplateMenu(Strategies, custom, HumanPlay, HumanMenu);

        // Customize Menu Option
        if(Template.Equals(custom))
        {
            bool agreeCustomization = false;
            do
            {
                CustomizeGame customizer = new CustomizeGame(Strategies, HumanPlay, HumanMenu);
                Template = customizer.Start(ref JustRound);

                agreeCustomization = Menus.MakeSureCostumizationMenu();

            } while (!agreeCustomization);
        }

        // READY FOR LAUNCH ~~~~~~~~~~
        Game = (JustRound) ? Template.Round : Template.Tournament;
        
        // show message
        QuickScreen quickScreen = new("All set, the game will start soon");
        quickScreen.Show(); 
        
        if(Game is Round) { RunRound(); } else{ RunTournament(); }

        Console.WriteLine("Press any key to end game");
        Console.ReadKey();

        if(PlayAgainMenu()) { Program.MainSkipIntro(); }
    }

    private void RunRound()
    {
        bool first = true;

        foreach (var step in Game.NextMove())
        {
            ShowRound(step, ref first);
        }
    }

    private void ShowRound(GameStatus step, ref bool first)
    {
        if(first) // just first game status
        {
            // Assign colors
            PlayersColors = Utils.Utils.AssignColors(step.Players!.ToArray());

            if(!HumanPlay)
            { ConsolePrinter.ShowPlayerTokens(step, PlayersColors); }

            first = false;
        }
        else
        {
            IPlay lastPlay = step.Plays!.Last();
            ConsolePrinter.PrintPlay(step, PlayersColors!);

            // wait two seconds
            if(HumanPlay) { Utils.Utils.Lapse(2); }
        }

        if(step.RoundOver) // just last game status
        {
            ConsolePrinter.PrintRoundWinner(step.RoundWinner!.Winner, step.RoundWinner.Score);
        }
    }

    private void RunTournament()
    {
        Tournament tournament = (Tournament)Game;

        int roundNumber = 1;
        bool firstInRound = true;
        
        // iterate through all game status
        foreach (var step in Game.NextMove())
        {
            if(firstInRound)
            {
                ConsolePrinter.ShowTournamentStatus(roundNumber, tournament.TeamsScore);
                ShowRound(step, ref firstInRound);
            }
            else
            {
                ShowRound(step, ref firstInRound);
            }

            if(step.TournamentOver) { ConsolePrinter.PrintTournamentWinner(step.TournamentWinner!); }
            
            if(step.RoundOver) 
            { 
                roundNumber++; 
                firstInRound = true;
            }
        }
    }

    private bool PlayAgainMenu()
    {
        SimpleOption playAgainOption = new SimpleOption("Play again");
        SimpleOption quitOption = new SimpleOption("Quit");

        List<SimpleOption> options = new List<SimpleOption>(){ playAgainOption, quitOption };
        SingleSelectionMenu<SimpleOption> menu = new SingleSelectionMenu<SimpleOption>(options, "THANKS FOR PLAYING DOMMIE, now what? ", false);
        menu.Show();

        return menu.Selected.Equals(playAgainOption);
    }

    private static Token_onBoard HumanMenu(BoardInfo info, List<Token> tokens, IPlayer player)
    {
        PlaySelectorMenu tokenSelectorMenu = new(tokens, info);
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
        if(info.Judge.IsValid(info.BoardTokens.Count, info.Ends, play))
        {
            return play;
        }
        else { return new Token_onBoard(tokenSelectorMenu.Selected, false, player, playRight); }
    }
}
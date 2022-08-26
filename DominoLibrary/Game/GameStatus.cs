using Utils;
namespace DominoLibrary;

public class GameStatus
{
    public CircularList<IPlayer>? Players {get; private set;} 
    public LinkedList<Token_onBoard>? BoardTokens {get; private set;}
    public Dictionary<IPlayer, List<Token>>? PlayersTokens {get; private set;}
    public int[]? Ends { get; private set;} = {-1, -1};
    public List<IPlay>? Plays { get; private set; }
    public List<Team>? Teams { get; private set;}
    public int MaxDouble { get; private set;}
    public bool RoundOver { get; private set;} = false;
    public WinnerStatus? RoundWinner { get; private set;} = null;
    public Judge? Judge { get; }

    public Dictionary<Team, int> TeamsScore { get; private set; }
    public int WinScore { get; private set; }
    public bool TournamentOver { get; private set;} = false;
    public WinnerStatus? TournamentWinner { get; private set;} = null;
    

    

    public GameStatus(List<Team> teams, CircularList<IPlayer> players, int maxDouble, GameStatus tournamentStatus, Judge judge, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        // Round
        Players = players;
        BoardTokens = new();
        Judge = judge;
        Plays = new();
        Teams = teams;
        MaxDouble = maxDouble;
        PlayersTokens = playersTokens;

        // Tourn
        WinScore = tournamentStatus.WinScore;
        TeamsScore = tournamentStatus.TeamsScore;
    }
    public GameStatus(int winScore, Dictionary<Team, int> teamsScores, WinnerStatus roundWinner)
    {
        WinScore = winScore;
        TeamsScore = teamsScores;
        RoundWinner = roundWinner;
        RoundOver = true;
    }

    private GameStatus(CircularList<IPlayer> players, LinkedList<Token_onBoard> boardTokens, int[] ends, List<IPlay> plays, List<Team> teams, int maxDouble, bool roundOver, 
        WinnerStatus roundWinner, Dictionary<Team, int> teamsScore, int winScore, bool tournOver, WinnerStatus tournWinner, Judge judge, Dictionary<IPlayer, List<Token>> playersTokens)
    {
        Players = players; BoardTokens = boardTokens; Ends = ends; Plays = plays; Teams = teams; MaxDouble = maxDouble; RoundOver = roundOver;
        RoundWinner = roundWinner; TeamsScore = teamsScore; WinScore = winScore; TournamentOver = tournOver; TournamentWinner = tournWinner; Judge = judge; PlayersTokens = playersTokens;

    }


    public GameStatus UpdateRoundStatus(IPlay play, bool roundOver, WinnerStatus winner)
    {
        GameStatus newGameStatus = this.Clone();

        if(roundOver)
        {
            newGameStatus.RoundOver = true;
            newGameStatus.RoundWinner = winner;
            return newGameStatus;
        }

        if(!(play is Pass))
        {
            if(play.PlayRight)
            {
                newGameStatus.BoardTokens!.AddLast((Token_onBoard)play);
            }
            else
            {
                newGameStatus.BoardTokens!.AddFirst((Token_onBoard)play);
            }

            newGameStatus.ResetEnds();

            newGameStatus.RefreshHands(play);
        }
        
        newGameStatus.Plays!.Add(play);

        return newGameStatus;
    }

    private void RefreshHands(IPlay play)
    {
        int index = 0;

        // find token in player hand to remove it
        for (int i = 0; i < PlayersTokens![play.Owner].Count; i++)
        {
            if((play.Left == PlayersTokens[play.Owner][i].Left) && (play.Right == PlayersTokens[play.Owner][i].Right))
            {
                index = i;
                break; 
            }
        }

        PlayersTokens[play.Owner].RemoveAt(index);
    }

    public GameStatus UpdateTournamentStatus(bool tournamentOver, WinnerStatus winner, GameStatus lastStatus)
    {
        GameStatus newGameStatus = new GameStatus(lastStatus.Teams!, lastStatus.Players!, lastStatus.MaxDouble, this, lastStatus.Judge!, new());
        
        // que borre las cosas de la ronda anterior
        if(tournamentOver)
        {
            newGameStatus.TournamentOver = true;
            newGameStatus.TournamentWinner = winner;
        }
        else
        {
            // add score to winner, if there is one
            if(lastStatus.RoundWinner!.Winner != null)
            {
                TeamsScore[lastStatus.RoundWinner!.Winner] += lastStatus.RoundWinner.Score;
            }

            // reset round
            newGameStatus.BoardTokens = new();
            newGameStatus.Ends = new int[]{ -1, -1};
            newGameStatus.Plays = new();
            newGameStatus.RoundOver = false;
            newGameStatus.RoundWinner = null;
        }
        return newGameStatus;
    }

    private void ResetEnds()
    {       
        Token_onBoard token = BoardTokens!.First!.Value; // tener en cuenta que al inicio de la partida la lista siempre sera null

        Ends![0] = (token.Straight) ? token.Left : token.Right;
    
        token = BoardTokens.Last!.Value;

        Ends[1] = (token.Straight) ? token.Right : token.Left;
    }

    public GameStatus Clone()
    {
        // players
        CircularList<IPlayer> players = null!;
        players = Players!.Clone();
        
        // board tokens
        LinkedList<Token_onBoard> newBoardTokens = new();
        List<Token_onBoard> boardTokens = BoardTokens!.ToList();
        foreach (var item in boardTokens)
        {
            newBoardTokens.AddLast(item);
        }

        int[] ends = Ends!.ToArray();

        // plays
        List<IPlay> plays = Plays!.ToList();
        
        List<Team> teams = Teams!.ToList();

        Dictionary<Team, int> teamsScore = TeamsScore.ToDictionary(x => x.Key, x => x.Value);

        Dictionary<IPlayer, List<Token>> playersTokens = PlayersTokens!.ToDictionary(x => x.Key, y => y.Value);

        return new GameStatus(players, newBoardTokens, ends, plays, teams, MaxDouble, RoundOver, RoundWinner!, teamsScore, WinScore, TournamentOver, TournamentWinner!, Judge!, playersTokens);
    }

}
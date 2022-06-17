namespace DominoLibrary;

public class Token
{
    public int Left { get; private set; }
    public int Right { get; private set; }
    

    public Token(int left, int right)
    {
        Left = left;
        Right = right;
    }
}

public class Token_onBoard : Token, IPlayable
{
    public bool Straight { get; private set; }
    public IPlayer Owner { get; private set; }
    public bool PlayRight { get; private set; }

    public Token_onBoard(int left, int right, bool straight, IPlayer owner, bool playRight) : base(left, right)
    {
        Straight = straight;
        Owner = owner;
        PlayRight = playRight;
    }

    public void PrintPlay(IPlayer player)
    {
        Console.ForegroundColor = ConsoleColor.Blue;

        string side = (PlayRight) ? "Right" : "Left";

        System.Console.WriteLine( (Straight) ?  $"{Owner.Name} played [{Left}:{Right}] by {side}" : $"{Owner.Name} played [{Right}:{Left}] by {side}" );
        Console.ForegroundColor = ConsoleColor.White;
    }
}

public interface IPlayable
{
    void PrintPlay(IPlayer player);
}

public class Pass : IPlayable
{
    public void PrintPlay(IPlayer player)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine($"{player.Name} pass");
    }
}
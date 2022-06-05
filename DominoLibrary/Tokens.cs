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

public class Token_onBoard : Token
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
}
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

    public bool EqualTokens(Token obj)
    {
        return (this.Left == obj.Left && this.Right == obj.Right);
    }
}

public class Token_onBoard : Token
{
    public bool Straight { get; private set; }
    public IPlayer Owner { get; private set; }
    public bool PlayRight { get; private set; }

    public Token_onBoard(Token token, bool straight, IPlayer owner, bool playRight) : base(token.Left, token.Right)
    {
        Straight = straight;
        Owner = owner;
        PlayRight = playRight;
    }
}


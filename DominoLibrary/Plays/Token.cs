namespace DominoLibrary;

public class Token : IComparable<Token>
{
    public int Left { get; private set; }
    public int Right { get; private set; }
    public int Points {get; private set;}
    
    public Token(int left, int right)
    {
        Left = left;
        Right = right;
        Points = Left + Right;
    }

    public bool EqualTokens(Token obj)
    {
        return (this.Left == obj.Left && this.Right == obj.Right);
    }

    public int CompareTo(Token? token)
    {
        if(token == null) return -1;
        return this.Points - token.Points;
    }

    public Token Clone() => new Token(Left, Right);

    public override string ToString() => $"[{this.Left}:{this.Right}]";
}





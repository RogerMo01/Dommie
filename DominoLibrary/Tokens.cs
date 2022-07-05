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

    public virtual bool IsPass() => false;

    public int CompareTo(Token token)
    {
        if(this.Points < token.Points) return -1;
        else if(this.Points > token.Points) return 1;
        else return 0;
    }

    public Token Clone() => new Token(Left, Right);
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

    public new Token_onBoard Clone() => new Token_onBoard(new Token(this.Left, this.Right), this.Straight, this.Owner, this.PlayRight);
}

// solo importa el par'ametro Owner & IsPass() method
public class Pass : Token_onBoard
{
    public Pass(Token token, bool straight, IPlayer owner, bool playRight) : base(token, straight, owner, playRight){}
    public override bool IsPass() => true;
}

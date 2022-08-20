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

    public int CompareTo(Token? token)
    {
        if(token == null) return -1;
        return this.Points - token.Points;
    }

    public Token Clone() => new Token(Left, Right);

    public override string ToString() => $"[{this.Left}:{this.Right}]";
}

public class Token_onBoard : Token, IPlay
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

    public new IPlay Clone() => new Token_onBoard(new Token(this.Left, this.Right), this.Straight, this.Owner, this.PlayRight);
    
    public override string ToString() => (Straight) ? $"[{Left}:{Right}]" : $"[{Right}:{Left}]";
}

public class Pass : IPlay
{
    public int Left { get => -1;}
    public int Right { get => -1;}
    public int Points { get => -1; }
    public bool Straight { get => true; }
    public IPlayer Owner { get ; }
    public bool PlayRight { get => true; }

    public override string ToString() => "PASS";

    public IPlay Clone() => new Pass(Owner);

    public Pass(IPlayer owner)
    {
        Owner = owner;
    }
}

namespace DominoLibrary;

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
namespace DominoLibrary;

class Token
{
    public int Left { get; private set; }
    public int Right { get; private set; }

    public Token(int left, int right)
    {
        Left = left;
        Right = right;
    }
    
}
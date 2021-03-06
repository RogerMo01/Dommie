namespace Utils;

public class Node<T> 
{
    public T Value { get; set; }
    public Node<T>? Next { get; set; }
    public Node<T>? Previous { get; set; }

    public Node(T value, Node<T> next, Node<T> previous)
    {
        Value = value;
        Next = next;
        Previous = previous;
    }

    public Node(T value)
    {
        Value = value;
    }
}
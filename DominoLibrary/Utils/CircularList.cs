using System.Collections;

namespace Utils;

public class CircularList<T> : IEnumerable<T>
{
    public Node<T> First { get; set; }
    public Node<T> Last { get; set; }
    public int Count { get; private set; }

    public CircularList(T initialValue)
    {
        Node<T> node = new Node<T>(initialValue);
        Count = 1;

        First = node;
        Last = node;

        node.Next = node;
        node.Previous = node;
    }

    public void AddLast(T item) 
    {
        Node<T> node = new Node<T>(item);
        AddLast(node);
    }
    public void AddLast(Node<T> node) 
    {
        First.Previous = node;
        Last.Next = node;
        node.Next = First;
        node.Previous = Last;

        Last = node;
        Count++;
    }

    public void AddFirst(T item)
    {
        Node<T> node = new Node<T>(item);
        AddFirst(node);
    }
    public void AddFirst(Node<T> node)
    {
        First.Previous = node;
        Last.Next = node;
        node.Next = First;
        node.Previous = Last;

        First = node;
        Count++;
    }

    public T[] ToArray()
    {
        Node<T> node = First;
        T[] result = new T[Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = node.Value;
            node = node.Next!;
        }

        return result;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new CircularEnumerator<T>(First);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
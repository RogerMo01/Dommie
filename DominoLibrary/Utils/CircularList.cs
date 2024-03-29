using System.Collections;

namespace Utils;

public class CircularList<T> : IEnumerable<T>
{
    public Node<T> First { get; set; }
    public Node<T> Last { get; set; }
    public int Count { get; private set; }
    IEnumerator<T> Enumerator;

    public CircularList(T initialValue) : this(new Node<T>(initialValue)){ }
    public CircularList(Node<T> initialNode)
    {
        Count = 1;

        First = initialNode;
        Last = initialNode;

        initialNode.Next = initialNode;
        initialNode.Previous = initialNode;

        Enumerator = new CircularEnumerator<T>(initialNode);
    }
    public CircularList(Node<T> initialNode, IEnumerator<T> enumerator) : this(initialNode)
    {
        Enumerator = enumerator;
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

    public Node<T> FindNode(T value)
    {
        Node<T> current = this.First;
        for (int i = 0; i < this.Count; i++)
        {
            if(value!.Equals(current.Value)) return current;
            current = current.Next!;
        }

        return null!;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        return Enumerator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public CircularList<T> Clone()
    {
        T[] arr = this.ToArray();
        Node<T> node = new Node<T>(arr[0]);
        CircularList<T> result = new CircularList<T>(node, new ClassicEnumerator<T>(node));
        for (int i = 1; i < Count; i++)
        {
            result.AddLast(arr[i]);
        }
        return result;
    }

    public CircularList<T> RotateTill(T item)
    {
        T[] items = this.ToArray();
        int index = 0;

        for (int i = 0; i < items.Length; i++)
        {
            if(items[i]!.Equals(item)) 
            {
                index = i;
                break;
            }
        }

        CircularList<T> result = new CircularList<T>(items[index]);

        for (int i = index + 1; i < items.Length; i++)
        {
            result.AddLast(items[i]);
        }
        for (int i = 0; i < index; i++)
        {
            result.AddLast(items[i]);
        }

        return result;
    }
}
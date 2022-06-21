using System.Collections;

namespace Utils;

class CircularEnumerator<T> : IEnumerator<T>
{
    Node<T> First;
    Node<T> CurrentNode;
    public CircularEnumerator(Node<T> first)
    {
        CurrentNode = first;
        First = first;
    }

    public T Current
    {
        get{
            return Current;
        }
    }

    object IEnumerator.Current
    {
        get{
            return Current!;
        }
    }

    public void Dispose(){}

    public bool MoveNext()
    {
        CurrentNode = CurrentNode.Next!;
        return true;
    }

    public void Reset()
    {
        CurrentNode = First;
    }
}
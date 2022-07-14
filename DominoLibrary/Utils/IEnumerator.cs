using System.Collections;

namespace Utils;

class CircularEnumerator<T> : IEnumerator<T>
{
    Node<T> First;
    Node<T> CurrentNode;
    public CircularEnumerator(Node<T> first)
    {
        CurrentNode = first.Previous!;
        First = first;
    }

    public T Current => CurrentNode.Value;

    object IEnumerator.Current => Current!;

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

class ClassicEnumerator<T> : IEnumerator<T>
{
    Node<T> First;
    Node<T> CurrentNode;
    int index = 0;
    public ClassicEnumerator(Node<T> first)
    {
        First = first;
        CurrentNode = first;
    }

    public T Current => CurrentNode.Value;

    object IEnumerator.Current => Current!;

    public void Dispose(){ }

    public bool MoveNext()
    {
        if(index == 0)
        {
            index++;
            return true;
        }  

        CurrentNode = CurrentNode.Next!;
        return !CurrentNode.Equals(First);
    }

    public void Reset()
    {
        CurrentNode = First;
        index = 0;
    }
}
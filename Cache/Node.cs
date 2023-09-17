namespace Cache;

public class Node<T>
{
    public string Key { get; set; }
    public T Value { get; set; }
    public Node<T>? Next { get; set; }
    public Node<T>? Previous { get; set; }
    
    public Node(string key, T value)
    {
        Key = key;
        Value = value;
    }
}

public class NodeWithFreq<T>
{
    public int Frequency;
    public LinkedList<string> Keys = new();
    public NodeWithFreq<T>? Prev = null, Next = null;
        
    public NodeWithFreq(int frequency) {
        Frequency = frequency;
    }
}
namespace Cache;

public class LfuCache<T>
{
    private NodeWithFreq<T>? _head;
    private readonly int _capacity;
    private readonly Dictionary<string, T> _keyToValue;
    private readonly Dictionary<string, NodeWithFreq<T>> _keyToNode;
    
    public LfuCache(int capacity)
    {
        _capacity = capacity;
        _keyToValue = new Dictionary<string, T>();
        _keyToNode = new Dictionary<string, NodeWithFreq<T>>();
    }
    
    public T? Get(string key)
    {
        if (!_keyToValue.ContainsKey(key))
        {
            return default;
        }
        
        IncreaseFreq(key);
        return _keyToValue[key];
    }
    
    public void Put(string key, T value) {
        if (_capacity == 0) return;
        if (_keyToValue.ContainsKey(key)) {
            _keyToValue.Add(key, value);
            IncreaseFreq(key); // updating an already existing key is also seen as accessing the key; // so increase frequency by 1
        } else {
            if (_keyToValue.Count >= _capacity) {
                RemoveOld(); // purge the least frequently used element
            }
            _keyToValue.Add(key, value);
            AddToHead(key);
        }
    }

    private void AddToHead(string key) {
        if (_head == null) {
            _head = new NodeWithFreq<T>(0);
            _head.Keys.AddLast(key);
        } else if (_head.Frequency > 0) {
            var node = new NodeWithFreq<T>(0);
            node.Keys.AddLast(key);
            node.Next = _head;
            _head.Prev = node;
            _head = node;
        } else if (_head.Frequency == 0) {
            _head.Keys.AddLast(key);
        }
        _keyToNode.Add(key, _head);      
    }
    private void IncreaseFreq(string key)
    {
        var node = _keyToNode[key];
        node.Keys.Remove(key);
        
        if (node.Next == null) {
            node.Next = new NodeWithFreq<T>(node.Frequency + 1);
            node.Next.Prev = node;
            node.Next.Keys.AddLast(key);
        } else if (node.Next.Frequency == node.Frequency + 1) {
            node.Next.Keys.AddLast(key);
        } else {
            var tmp = new NodeWithFreq<T>(node.Frequency + 1);
            tmp.Keys.AddLast(key);
            tmp.Prev = node;
            tmp.Next = node.Next;
            node.Next.Prev = tmp;
            node.Next = tmp;
        }

        _keyToNode[key] = node.Next;
        if (!node.Keys.Any()) Remove(node);
    }
    
    private void RemoveOld() {
        if (_head == null) return;
        var old = "";
        foreach (var n in _head.Keys)
        {
            old = n;
            break;
        }
        
        _head.Keys.Remove(old);
        if (_head.Keys.Count == 0) Remove(_head);
        _keyToNode.Remove(old);
        _keyToValue.Remove(old);
    }
    
    private void Remove(NodeWithFreq<T> node) {
        if (node.Prev == null) {
            _head = node.Next;
        } else {
            node.Prev.Next = node.Next;
        } 
        if (node.Next != null) {
            node.Next.Prev = node.Prev;
        }
    }
}
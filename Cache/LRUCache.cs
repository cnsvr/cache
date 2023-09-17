namespace Cache;

public class LruCache<T> where T : class
{
    private readonly int _size;
    private int _count;
    private readonly Dictionary<string, Node<T>> _cache;
    private Node<T>? _head;
    private Node<T>? _tail;
    
    public LruCache(int size)
    {
        _size = size;
        _cache = new Dictionary<string, Node<T>>();
        _count = 0;
        _head = null;
        _tail = null;
    }
    
    public T? Get(string key)
    {
        /*
        Algorithm:
        STEP #1. In this method we are returning content of key
                 if it is present or else return null.

        STEP #2. If the key was present,
        since we used it it becomes the
        most recently used item. So remove the node which
        contains the key and
        make the node the head of the doubly linked list.
        */
        if (!_cache.ContainsKey(key))
        {
            return null;
        }
        
        var node = _cache[key];
        Remove(node);
        AttachFront(node);

        return node.Value;
    }
    
    public void Put(string key, T value)
    {
        /*
         Algorithm:
         1. If the key is already present then update the value and
            update the list similar to STEP #2 of getContent().

         2. If list reaches the capacity, then
            remove the LRU (which is the tail of the doubly linked list) from
            doubly linked list and make corresponding removal in hashmap too.
            Now add the add a new node for the new (pageNumber, pageContent)
            pair and make it head of the doubly linked list. Make
            corresponding entry in hashmap.

         3. If it fails in any of those conditions, then just
            add a new node for the new (pageNumber, pageContent)
            pair and make it head of the doubly linked list. Make
            corresponding entry in hashmap
        */

        if(_cache.ContainsKey(key)){
            var node = _cache[key];
            node.Value = value;  //refresh content;
            Remove(node);
            AttachFront(node);
            _cache.Add(key, node);
            return;
        }
        _count++;
        var newNode = new Node<T>(key, value);
        
        if (_count == 1){
            newNode.Previous = null;
            newNode.Next = null;
            _head = newNode;
            _tail = newNode;
            _cache.Add(key, newNode);
            return;
        }

        AttachFront(newNode);
        _cache.Add(key, newNode);

        if (_count <= _size) return;
        _cache.Remove(_tail!.Key);
        Remove(_tail);
        _count--;
    }
    
    private void Remove(Node<T> node)
    {
        if(node.Key == _tail!.Key){
            node.Previous!.Next = null;
            _tail = _tail.Previous;
            return;
        }
        node.Previous!.Next = node.Next;
        node.Next!.Previous = node.Previous;
    }
    
    private void AttachFront(Node<T> node)
    {
        node.Next = _head;
        node.Previous = null;
        if (_head != null)
        {
            _head.Previous = node;
        }
        _head = node;
        _tail ??= _head;
    }
}
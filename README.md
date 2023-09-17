### Least Recently Used (LRU) Cache
## Problem Statement:
- Design and implement a data structure for Least Recently Used (LRU) cache. It should support the following operations: get and put.

- getContent(key) - Get the value of the key if the key exists in the cache.

- insertContent(key, value) - Set or insert the value if the key is not already present. When the cache reached its capacity, it **should invalidate the least recently used item** before inserting a new item.

## Solution:
The first and most important purpose for any cache is providing faster and effective way for retrieval of data. So it is required that cache insert and retrieve operations should be fast , preferably O(1) time.

As we learnt, The LRU cache evicts the least recently used item first so we are required to :
- Track the recently used entries. 
- Track the entries which are not used since long time.
- Additionally, we need to make the insertion, retrieval operations fast.



So, we want the faster retrieval operation say taking only O(1) time, HashMap is a very efficient data structure. HashMap provides amortized O(1) for lookup and insertion as well. So HashMap solves the problem of faster operations. Remember that in case of collision the time complexity of HashMap operations worsens to O(total number of items in the hashmap).

However, HashMap does not provide functionality to track the recently used entries. So we need another data structure which can track the entries and still provide faster operations. The data structure to fulfill this requirement in Doubly Linked List as it has nodes which contains address and Doubly Linked List also takes O(1) for insertion, deletion and updation provided address of node is available. So By combining HashMap and Doubly Linked List, we will implement our LRU Cache in Java. Doubly Linked List will hold the values of the key and HashMap will hold the addresses and keys of the nodes of the list. 

![](https://thealgoristsblob.blob.core.windows.net/thealgoristsimages/lru.png)

We will follow the below approach in our code :
- We would make sure that at all time the head contains the most recently used item and the tail contains the least recently used or LRU item. So we will always remove the item from tail of the doubly linkedlist and will add element at the head of the doubly linkedlist.

## Time Complexity:
- Get operation: O(1)
- Put operation: O(1)


### Least Frequently Used (LFU) Cache
## Problem Statement:
- Design and implement a data structure for Least Frequently Used (LFU) cache. It should support the following operations: get and put.

- get(key) - Get the value (will always be positive) of the key if the key exists in the cache, otherwise return -1.

- put(key, value) - Set or insert the value if the key is not already present. When the cache reaches its capacity, it should invalidate the least frequently used item before inserting a new item. For the purpose of this problem, when there is a tie (i.e., two or more keys that have the same frequency), the least recently used key would be evicted.

- Both get and put must be O(1). 

## Solution:
- We will be using a DOUBLY LINKED LIST whose head would have all the keys with the least frequency, and the tail contains all the keys with the greatest frequency. The nodes are in the sorted order, sorted on the frequency. each node of the doubly linked list contains all a linkedhashset (you can also use arraylist) containing all the keys with the same frequency. The first element (at index 0) is the LEAST RECENTLY USED. (convince yourself) We keep a map to keep track of which key goes to which doubly linkedlist node.
- We keep another map just to map the key to the value. 

## Time Complexity:
- Get operation: O(1)
- Put operation: O(1) 

## Drawbacks of LFU Cache in general:
- While the LFU method may seem like an intuitive approach to memory management it is not without faults. Consider an item in memory which is referenced repeatedly for a short period of time and is not accessed again for an extended period of time. Due to how rapidly it was just accessed its counter has increased drastically even though it will not be used again for a decent amount of time. This leaves other blocks which may actually be used more frequently susceptible to purging simply because they were accessed through a different method.
- Moreover, new items that just entered the cache are subject to being removed very soon again, because they start with a low counter, even though they might be used very frequently after that. Due to major issues like these, a pure LFU system is fairly uncommon; instead, there are hybrids that utilize LFU concepts. 

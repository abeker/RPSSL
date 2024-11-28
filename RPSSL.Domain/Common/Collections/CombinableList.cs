using System.Collections;
using CSharpFunctionalExtensions;

namespace RPSSL.Domain.Common.Collections;

public class CombinableList<T> : ICombine, IList<T>
{
    private readonly List<T> items = [];

    public CombinableList()
    {
    }

    public CombinableList(IEnumerable<T> items)
    {
        this.items.AddRange(items);
    }

    public CombinableList(T item)
    {
        items.Add(item);
    }

    public ICombine Combine(ICombine item)
    {
        var combinedItems = new List<T>(items);
        combinedItems.AddRange(((CombinableList<T>)item).items);
        return CreateCombinedList(combinedItems);
    }

    public T this[int index]
    {
        get => items[index];
        set => items[index] = value;
    }

    public int Count => items.Count;

    public bool IsReadOnly => true;

    public void Add(T item) => items.Add(item);

    public void Clear() => items.Clear();

    public bool Contains(T item) => items.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

    public int IndexOf(T item) => items.IndexOf(item);

    public void Insert(int index, T item) => items.Insert(index, item);

    public bool Remove(T item) => items.Remove(item);

    public void RemoveAt(int index) => items.RemoveAt(index);

    protected virtual ICombine CreateCombinedList(IEnumerable<T> listItems)
        => new CombinableList<T>(listItems);

    IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
}
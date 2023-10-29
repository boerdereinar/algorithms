using System.Diagnostics.CodeAnalysis;

namespace SortingAlgorithms.DataStructures.Heaps;

/// <summary>
/// Represents the heap implemented in <see cref="PriorityQueue{TElement,TPriority}"/>.
/// </summary>
public sealed class DefaultHeap : IHeap
{
	/// <inheritdoc />
	public static IHeap<TSource, TKey> Create<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return DefaultHeap<TSource, TKey>.Create(source, keySelector, comparer);
	}
}

/// <summary>
/// Represents the heap implemented in <see cref="PriorityQueue{TElement,TPriority}"/>.
/// </summary>
/// <typeparam name="TSource">The type of the elements in the heap.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
public sealed class DefaultHeap<TSource, TKey> : IHeap<TSource, TKey>
{
	private readonly PriorityQueue<TSource, TKey> _queue;

	/// <summary>
	/// Initializes a new instance of the <see cref="DefaultHeap{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public DefaultHeap(IComparer<TKey> comparer)
	{
		_queue = new(comparer);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="DefaultHeap{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">The function used to extract the key from the element.</param>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public DefaultHeap(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		_queue = new(source.Select(x => (x, keySelector(x))), comparer);
	}

	/// <inheritdoc />
	public int Count => _queue.Count;

	/// <inheritdoc />
	public TSource Min => _queue.Peek();

	/// <inheritdoc />
	public void Insert(TSource value, TKey key)
	{
		_queue.Enqueue(value, key);
	}

	/// <inheritdoc />
	public bool TryGetMin([MaybeNullWhen(false)] out TSource value, [MaybeNullWhen(false)] out TKey key)
	{
		return _queue.TryPeek(out value, out key);
	}

	/// <inheritdoc />
	public TSource DeleteMin()
	{
		return _queue.Dequeue();
	}

	/// <inheritdoc />
	public bool TryDeleteMin([MaybeNullWhen(false)] out TSource value, [MaybeNullWhen(false)] out TKey key)
	{
		return _queue.TryDequeue(out value, out key);
	}

	/// <inheritdoc />
	public static IHeap<TSource, TKey> Create(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return new DefaultHeap<TSource, TKey>(source, keySelector, comparer);
	}
}

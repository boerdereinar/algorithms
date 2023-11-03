using System.Diagnostics.CodeAnalysis;

namespace Algorithms.DataStructures.Heaps;

/// <summary>
/// Represents the heap implemented in <see cref="PriorityQueue{TElement,TPriority}"/>.
/// </summary>
public sealed class DefaultHeap : IHeap
{
	private DefaultHeap() { }

	/// <inheritdoc />
	public static IHeap<TValue, TKey> Create<TValue, TKey>(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return DefaultHeap<TValue, TKey>.Create(source, keySelector, comparer);
	}
}

/// <summary>
/// Represents the heap implemented in <see cref="PriorityQueue{TElement,TPriority}"/>.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the heap.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
public sealed class DefaultHeap<TValue, TKey> : IHeap<TValue, TKey>
{
	private readonly PriorityQueue<TValue, TKey> _queue;

	/// <summary>
	/// Initializes a new instance of the <see cref="DefaultHeap{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public DefaultHeap(IComparer<TKey> comparer)
	{
		_queue = new(comparer);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="DefaultHeap{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">The function used to extract the key from the element.</param>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public DefaultHeap(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		_queue = new(source.Select(x => (x, keySelector(x))), comparer);
	}

	/// <inheritdoc />
	public int Count => _queue.Count;

	/// <inheritdoc />
	public TValue Min => _queue.Peek();

	/// <inheritdoc />
	public void Insert(TValue value, TKey key)
	{
		_queue.Enqueue(value, key);
	}

	/// <inheritdoc />
	public bool TryGetMin([MaybeNullWhen(false)] out TValue value, [MaybeNullWhen(false)] out TKey key)
	{
		return _queue.TryPeek(out value, out key);
	}

	/// <inheritdoc />
	public TValue DeleteMin()
	{
		return _queue.Dequeue();
	}

	/// <inheritdoc />
	public bool TryDeleteMin([MaybeNullWhen(false)] out TValue value, [MaybeNullWhen(false)] out TKey key)
	{
		return _queue.TryDequeue(out value, out key);
	}

	/// <inheritdoc />
	public static IHeap<TValue, TKey> Create(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return new DefaultHeap<TValue, TKey>(source, keySelector, comparer);
	}
}

using System.Diagnostics.CodeAnalysis;
using Algorithms.Common.Collections;

namespace Algorithms.DataStructures.Heaps;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Binary_heap">Binary Heap</a> data structure.
/// </summary>
public sealed class BinaryHeap : IHeap
{
	/// <inheritdoc />
	public static IHeap<TSource, TKey> Create<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return BinaryHeap<TSource, TKey>.Create(source, keySelector, comparer);
	}
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Binary_heap">Binary Heap</a> data structure.
/// </summary>
/// <typeparam name="TSource">The type of the elements in the heap.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
public sealed class BinaryHeap<TSource, TKey> : IHeap<TSource, TKey>
{
	private readonly KeyedArray<TSource, TKey> _source;

	/// <summary>
	/// Initializes a new instance of the <see cref="BinaryHeap{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public BinaryHeap(IComparer<TKey> comparer)
	{
		_source = new(4, comparer);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="BinaryHeap{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="source">The source array.</param>
	/// <param name="keySelector">The function used to extract the key from the element.</param>
	/// <param name="comparer">The comparer used to compare keys.</param>
	/// <remarks>The source array will <b>not</b> be copied and all heap operations will be done in-place.</remarks>
	public BinaryHeap(TSource[] source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		: this(new KeyedArray<TSource, TKey>(source, keySelector, comparer)) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="BinaryHeap{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="source">The source array.</param>
	/// <remarks>The source array will <b>not</b> be copied and all heap operations will be done in-place.</remarks>
	internal BinaryHeap(KeyedArray<TSource, TKey> source)
	{
		_source = source;
		Count = source.Length;

		for (var i = Count / 2 - 1; i >= 0; i--)
			Balance(i);
	}

	/// <inheritdoc />
	public int Count { get; private set; }

	/// <inheritdoc />
	public TSource Min => Count > 0 ? _source.Element(0) : throw new InvalidOperationException("The heap is empty.");

	/// <inheritdoc />
	public void Insert(TSource value, TKey key)
	{
		if (_source.Length == Count)
			_source.Resize(Math.Max(1, _source.Length) * 4);

		_source[Count++] = (value, key);
		for (var i = Count / 2 - 1; i >= 0; i--)
			Balance(i);
	}

	/// <inheritdoc />
	public bool TryGetMin([MaybeNullWhen(false)] out TSource value, [MaybeNullWhen(false)] out TKey key)
	{
		if (Count == 0)
		{
			value = default;
			key = default;
			return false;
		}

		(value, key) = _source[0];
		return true;
	}

	/// <inheritdoc />
	public TSource DeleteMin()
	{
		return TryDeleteMin(out var value, out _) ? value : throw new InvalidOperationException("The heap is empty.");
	}

	/// <inheritdoc />
	public bool TryDeleteMin([MaybeNullWhen(false)] out TSource value, [MaybeNullWhen(false)] out TKey key)
	{
		if (Count == 0)
		{
			value = default;
			key = default;
			return false;
		}

		(value, key) = _source[0];

		_source[0] = _source[--Count];
		Balance(0);

		return true;
	}

	/// <inheritdoc />
	public static IHeap<TSource, TKey> Create(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return new BinaryHeap<TSource, TKey>(source.ToArray(), keySelector, comparer);
	}

	private void Balance(int i)
	{
		for (var root = i; 2 * root + 1 < Count;)
		{
			var left = 2 * root + 1;
			var swap = root;

			if (_source.Compare(swap, left) > 0)
				swap = left;

			if (left + 1 < Count && _source.Compare(swap, left + 1) > 0)
				swap = left + 1;

			if (swap == root)
				return;

			_source.Swap(root, swap);
			root = swap;
		}
	}
}

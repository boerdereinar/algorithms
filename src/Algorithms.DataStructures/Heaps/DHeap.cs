using System.Diagnostics.CodeAnalysis;
using Algorithms.Common.Collections;

namespace Algorithms.DataStructures.Heaps;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/D-ary_heap">D-heap</a> data structure.
/// </summary>
public sealed class DHeap : IHeap
{
	/// <inheritdoc />
	public static IHeap<TValue, TKey> Create<TValue, TKey>(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return DHeap<TValue, TKey>.Create(source, keySelector, comparer);
	}

	/// <summary>
	/// Creates a min-heap from a collection.
	/// </summary>
	/// <param name="d">The number of children in each node.</param>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <typeparam name="TValue">The type of the elements in the heap.</typeparam>
	/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
	/// <returns>The <see cref="IHeap{TValue,TKey}"/>.</returns>
	public static IHeap<TValue, TKey> Create<TValue, TKey>(int d, IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return DHeap<TValue, TKey>.Create(d, source, keySelector, comparer);
	}
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/D-ary_heap">D-heap</a> data structure.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the heap.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
public sealed class DHeap<TValue, TKey> : IHeap<TValue, TKey>
{
	private readonly int _d;
	private readonly KeyedArray<TValue, TKey> _source;

	/// <summary>
	/// Initializes a new instance of the <see cref="DHeap{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public DHeap(IComparer<TKey> comparer) : this(2, comparer) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="DHeap{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="d">The number of children in each node.</param>
	/// <param name="comparer">The comparer used to compare keys.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="d"/> is less than 2.</exception>
	public DHeap(int d, IComparer<TKey> comparer)
	{
		if (d < 2)
			throw new ArgumentOutOfRangeException(nameof(d), d, "The number of children in each node must be at least 2.");

		_d = d;
		_source = new(4, comparer);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="DHeap{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="source">The source array.</param>
	/// <param name="keySelector">The function used to extract the key from the element.</param>
	/// <param name="comparer">The comparer used to compare keys.</param>
	/// <remarks>The source array will <b>not</b> be copied and all heap operations will be done in-place.</remarks>
	public DHeap(TValue[] source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
		: this(2, new KeyedArray<TValue, TKey>(source, keySelector, comparer)) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="DHeap{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="d">The number of children in each node.</param>
	/// <param name="source">The source array.</param>
	/// <param name="keySelector">The function used to extract the key from the element.</param>
	/// <param name="comparer">The comparer used to compare keys.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="d"/> is less than 2.</exception>
	/// <remarks>The source array will <b>not</b> be copied and all heap operations will be done in-place.</remarks>
	public DHeap(int d, TValue[] source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
		: this(d, new KeyedArray<TValue, TKey>(source, keySelector, comparer)) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="DHeap{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="d">The number of children in each node.</param>
	/// <param name="source">The source array.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="d"/> is less than 2.</exception>
	/// <remarks>The source array will <b>not</b> be copied and all heap operations will be done in-place.</remarks>
	internal DHeap(int d, KeyedArray<TValue, TKey> source)
	{
		if (d < 2)
			throw new ArgumentOutOfRangeException(nameof(d), d, "The number of children in each node must be at least 2.");

		_d = d;
		_source = source;
		Count = source.Length;

		for (var i = Count / 2 - 1; i >= 0; i--)
			SiftDown(i);
	}

	/// <inheritdoc />
	public int Count { get; private set; }

	/// <inheritdoc />
	public TValue Min => Count > 0 ? _source.Element(0) : throw new InvalidOperationException("The heap is empty.");

	/// <inheritdoc />
	public void Insert(TValue value, TKey key)
	{
		if (_source.Length == Count)
			_source.Resize(Math.Max(1, _source.Length) * 4);

		_source[Count++] = (value, key);
		SiftUp(Count - 1);
	}

	/// <inheritdoc />
	public bool TryGetMin([MaybeNullWhen(false)] out TValue value, [MaybeNullWhen(false)] out TKey key)
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
	public TValue DeleteMin()
	{
		return TryDeleteMin(out var value, out _) ? value : throw new InvalidOperationException("The heap is empty.");
	}

	/// <inheritdoc />
	public bool TryDeleteMin([MaybeNullWhen(false)] out TValue value, [MaybeNullWhen(false)] out TKey key)
	{
		if (Count == 0)
		{
			value = default;
			key = default;
			return false;
		}

		(value, key) = _source[0];

		_source[0] = _source[--Count];
		SiftDown(0);

		return true;
	}

	/// <inheritdoc />
	public static IHeap<TValue, TKey> Create(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return new DHeap<TValue, TKey>(source.ToArray(), keySelector, comparer);
	}

	/// <summary>
	/// Creates a min-heap from an array of elements.
	/// </summary>
	/// <param name="d">The number of children in each node.</param>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <returns>The min-heap.</returns>
	public static IHeap<TValue, TKey> Create(int d, IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return new DHeap<TValue, TKey>(d, source.ToArray(), keySelector, comparer);
	}

	private void SiftUp(int i)
	{
		for (var j = (i - 1) / _d; j >= 0;)
		{
			if (_source.Compare(i, j) >= 0)
				break;

			_source.Swap(i, j);
			i = j;
			j = (i - 1) / _d;
		}
	}

	private void SiftDown(int i)
	{
		while (true)
		{
			var left = _d * i + 1;
			if (left >= Count)
				break;

			var maxChild = left;
			for (var j = 1; j < _d && left + j < Count; j++)
				if (_source.Compare(maxChild, left + j) > 0)
					maxChild = left + j;

			if (_source.Compare(i, maxChild) > 0)
				_source.Swap(i, maxChild);

			i = maxChild;
		}
	}
}

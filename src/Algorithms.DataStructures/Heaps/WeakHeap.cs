using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Algorithms.Common.Collections;

namespace Algorithms.DataStructures.Heaps;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Weak_heap">Weak Heap</a> data structure.
/// </summary>
public sealed class WeakHeap : IHeap
{
	[ExcludeFromCodeCoverage]
	private WeakHeap() { }

	/// <inheritdoc />
	public static IHeap<TValue, TKey> Create<TValue, TKey>(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return WeakHeap<TValue, TKey>.Create(source, keySelector, comparer);
	}
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Weak_heap">Weak Heap</a> data structure.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the heap.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
public sealed class WeakHeap<TValue, TKey> : IHeap<TValue, TKey>
{
	private readonly KeyedArray<TValue, TKey> _source;
	private readonly BitArray _r;

	/// <summary>
	/// Initializes a new instance of the <see cref="WeakHeap{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public WeakHeap(IComparer<TKey> comparer)
	{
		_source = new(4, comparer);
		_r = new(4);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="WeakHeap{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="source">The source array.</param>
	/// <param name="keySelector">The function used to extract the key from the element.</param>
	/// <param name="comparer">The comparer used to compare keys.</param>
	/// <remarks>The source array will <b>not</b> be copied and all heap operations will be done in-place.</remarks>
	public WeakHeap(TValue[] source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
		: this(new KeyedArray<TValue, TKey>(source, keySelector, comparer)) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="WeakHeap{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="source">The source array.</param>
	/// <remarks>The source array will <b>not</b> be copied and all heap operations will be done in-place.</remarks>
	internal WeakHeap(KeyedArray<TValue, TKey> source)
	{
		_source = source;
		_r = new(source.Length);
		Count = source.Length;

		for (var i = Count - 1; i > 0; i--)
			Merge(i, DistinguishedAncestor(i));
	}

	/// <inheritdoc />
	public int Count { get; private set; }

	/// <inheritdoc />
	public TValue Min => Count > 0 ? _source.Element(0) : throw new InvalidOperationException("The heap is empty.");

	/// <inheritdoc />
	public void Insert(TValue value, TKey key)
	{
		if (_source.Length == Count)
		{
			var length = Math.Max(1, _source.Length) * 4;
			_source.Resize(length);
			_r.Length = length;
		}

		_source[Count] = (value, key);
		if (int.IsEvenInteger(Count))
			_r[Count / 2] = false;

		SiftUp(Count++);
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
		if (Count > 1)
			SiftDown(0);

		return true;
	}

	/// <inheritdoc />
	public static IHeap<TValue, TKey> Create(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return new WeakHeap<TValue, TKey>(source.ToArray(), keySelector, comparer);
	}

	/// <summary>
	/// Find the distinguished ancestor of <paramref name="i"/>.
	/// The distinguished ancestor of <paramref name="i"/> is the parent of <paramref name="i"/> if <paramref name="i"/>
	/// is the right child of its parent, or <paramref name="i"/> otherwise.
	/// </summary>
	/// <param name="i">The index.</param>
	/// <returns>The distinguished ancestor of <paramref name="i"/>.</returns>
	private int DistinguishedAncestor(int i)
	{
		while (int.IsOddInteger(i) == _r[i /= 2]) { }
		return i;
	}

	private bool Merge(int i, int j)
	{
		if (_source.Compare(i, j) >= 0)
			return true;

		_source.Swap(i, j);
		_r[i] ^= true;
		return false;
	}

	private void SiftUp(int i)
	{
		int j;
		while (i != 0 && !Merge(i, j = DistinguishedAncestor(i)))
			i = j;
	}

	private void SiftDown(int i)
	{
		var k = 2 * i + 1 - (_r[i] ? 1 : 0);
		int newK;
		while ((newK = 2 * k + (_r[k] ? 1 : 0)) < Count)
			k = newK;

		for (; k > i; k >>= 1)
			Merge(k, i);
	}
}

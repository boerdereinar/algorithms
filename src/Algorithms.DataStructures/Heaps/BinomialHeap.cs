using System.Diagnostics.CodeAnalysis;
using Algorithms.DataStructures.Trees;

namespace Algorithms.DataStructures.Heaps;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Binomial_heap">Binomial Heap</a> data structure.
/// </summary>
public sealed class BinomialHeap : IHeap
{
	/// <inheritdoc />
	public static IHeap<TSource, TKey> Create<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return BinomialHeap<TSource, TKey>.Create(source, keySelector, comparer);
	}
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Binomial_heap">Binomial Heap</a> data structure.
/// </summary>
/// <typeparam name="TSource">The type of the elements in the heap.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
public sealed class BinomialHeap<TSource, TKey> : IHeap<TSource, TKey>
{
	private readonly IComparer<TKey> _comparer;
	private readonly Dictionary<int, BinomialTree<TSource, TKey>> _trees = new();

	private int _min = -1;

	/// <summary>
	/// Initializes a new instance of the <see cref="BinomialHeap{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public BinomialHeap(IComparer<TKey> comparer)
	{
		_comparer = comparer;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="BinomialHeap{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">The function used to extract the key from the element.</param>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public BinomialHeap(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		_comparer = comparer;

		foreach (var item in source)
			Insert(item, keySelector(item));
	}

	/// <inheritdoc />
	public int Count { get; private set; }

	/// <inheritdoc />
	public TSource Min => TryGetMin(out var value, out _) ? value : throw new InvalidOperationException("The heap is empty.");

	/// <inheritdoc />
	public void Insert(TSource value, TKey key)
	{
		Count++;
		Insert(new(value, key, _comparer));
	}

	/// <inheritdoc />
	public bool TryGetMin([MaybeNullWhen(false)] out TSource value, [MaybeNullWhen(false)] out TKey key)
	{
		if (_min < 0)
		{
			value = default;
			key = default;
			return false;
		}

		value = _trees[_min].Value;
		key = _trees[_min].Key;
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
		if (_min < 0)
		{
			value = default;
			key = default;
			return false;
		}

		var tree = _trees[_min];
		value = tree.Value;
		key = tree.Key;
		_trees.Remove(_min);
		Count--;

		_min = _trees.Count == 0 ? -1 : _trees.MinBy(x => x.Value.Key, _comparer).Key;
		foreach (var item in tree.Forest)
			Insert(item);

		return true;
	}

	/// <inheritdoc />
	public static IHeap<TSource, TKey> Create(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return new BinomialHeap<TSource, TKey>(source, keySelector, comparer);
	}

	private void Insert(BinomialTree<TSource, TKey> value)
	{
		while (_trees.TryGetValue(value.Order, out var tree))
		{
			if (_min == value.Order)
				_min++;

			_trees.Remove(value.Order);
			value = BinomialTree<TSource, TKey>.Merge(value, tree);
		}

		_trees[value.Order] = value;
		if (_min == value.Order || _min < 0 || _comparer.Compare(value.Key, _trees[_min].Key) < 0)
			_min = value.Order;
	}
}

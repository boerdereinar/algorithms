using Algorithms.DataStructures.Trees;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Insertion;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/B-tree">B-Tree Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class BTreeSort<TKey> : TreeSort<TKey, BTree>
{
	private readonly int _m;

	/// <summary>
	/// Initializes a new instance of the <see cref="BTreeSort{TKey}"/> class.
	/// </summary>
	public BTreeSort() : this(2) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="BTreeSort{TKey}"/> class.
	/// </summary>
	/// <param name="m">The order of the tree.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="m"/> is less than 1.</exception>
	public BTreeSort(int m)
	{
		if (m < 1)
			throw new ArgumentOutOfRangeException(nameof(m), m, "The order of the tree must be at least 1.");

		_m = m;
	}

	/// <inheritdoc cref="TreeSort{TKey,THeap}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new BTreeSort<TKey>();

	/// <inheritdoc />
	public override IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return BTree.Create(_m, source, keySelector, comparer);
	}

	/// <inheritdoc />
	public override ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new BTreeSort<T>(_m);
	}
}

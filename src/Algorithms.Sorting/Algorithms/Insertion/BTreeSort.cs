using Algorithms.DataStructures.Trees;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Insertion;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/B-tree">B-Tree Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class BTreeSort<TKey> : TreeSort<TKey, BTree, int>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="BTreeSort{TKey}"/> class.
	/// </summary>
	public BTreeSort() { }

	/// <summary>
	/// Initializes a new instance of the <see cref="BTreeSort{TKey}"/> class.
	/// </summary>
	/// <param name="m">The order of the tree.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="m"/> is less than 1.</exception>
	public BTreeSort(int m) : base(m)
	{
		if (m < 1)
			throw new ArgumentOutOfRangeException(nameof(m), m, "The order of the tree must be at least 1.");
	}

	/// <inheritdoc cref="TreeSort{TKey,THeap}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new BTreeSort<TKey>();
}

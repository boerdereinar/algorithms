using Algorithms.DataStructures.Trees;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Insertion;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Red%E2%80%93black_tree">Red-Black Tree Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class RedBlackTreeSort<TKey> : TreeSort<TKey, RedBlackTree>
{
	/// <inheritdoc cref="TreeSort{TKey,TTree}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new RedBlackTreeSort<TKey>();
}

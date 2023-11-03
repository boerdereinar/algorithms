using Algorithms.DataStructures.Trees;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Insertion;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Tree_sort">Binary Tree Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class BinaryTreeSort<TKey> : TreeSort<TKey, BinaryTree>
{
	/// <inheritdoc cref="TreeSort{TKey,TTree}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new BinaryTreeSort<TKey>();
}

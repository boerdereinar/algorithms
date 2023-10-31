using Algorithms.DataStructures.Trees;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Insertion;

/// <summary>
/// Represents a generic <a href="https://en.wikipedia.org/wiki/Tree_sort">Tree Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
/// <typeparam name="TTree">The type of the tree.</typeparam>
public class TreeSort<TKey, TTree> : ISortingAlgorithm<TKey> where TTree : ITraversableTree
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new TreeSort<TKey, TTree>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return TTree.Create(source, keySelector, comparer) ?? Enumerable.Empty<TSource>();
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new TreeSort<T, TTree>();
	}
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Tree_sort">Binary Tree Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class BinaryTreeSort<TKey> : TreeSort<TKey, BinaryTree>
{
	/// <inheritdoc cref="TreeSort{TKey,TTree}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new BinaryTreeSort<TKey>();
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Cartesian_tree#In_sorting">Cartesian Tree Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class CartesianTreeSort<TKey> : TreeSort<TKey, CartesianTree>
{
	/// <inheritdoc cref="TreeSort{TKey,TTree}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new CartesianTreeSort<TKey>();
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Red%E2%80%93black_tree">Red-Black Tree Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class RedBlackTreeSort<TKey> : TreeSort<TKey, RedBlackTree>
{
	/// <inheritdoc cref="TreeSort{TKey,TTree}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new RedBlackTreeSort<TKey>();
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Splaysort">Splaysort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of the elements to be sorted.</typeparam>
public sealed class SplaySort<TKey> : TreeSort<TKey, SplayTree>
{
	/// <inheritdoc cref="TreeSort{TKey,TTree}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new SplaySort<TKey>();
}

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
	public virtual IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return TTree.Create(source, keySelector, comparer) ?? Enumerable.Empty<TSource>();
	}

	/// <inheritdoc />
	public virtual ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new TreeSort<T, TTree>();
	}
}

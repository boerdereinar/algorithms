using System.Diagnostics.CodeAnalysis;
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
/// Represents a generic <a href="https://en.wikipedia.org/wiki/Tree_sort">Tree Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
/// <typeparam name="TTree">The type of the tree.</typeparam>
/// <typeparam name="TArgument">The type of the argument of the tree.</typeparam>
public class TreeSort<TKey, TTree, TArgument> : ISortingAlgorithm<TKey> where TTree : ITraversableTree<TArgument>
{
	/// <summary>
	/// Gets the argument of the tree sort.
	/// </summary>
	public TArgument? Argument { get; }

	/// <summary>
	/// Gets a value indicating whether the tree sort has an argument.
	/// </summary>
	[MemberNotNullWhen(true, nameof(Argument))]
	public bool HasArgument { get; }

	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new TreeSort<TKey, TTree, TArgument>();

	/// <summary>
	/// Initializes a new instance of the <see cref="TreeSort{TKey, TTree, TArgument}"/> class.
	/// </summary>
	public TreeSort() { }

	/// <summary>
	/// Initializes a new instance of the <see cref="TreeSort{TKey, TTree, TArgument}"/> class.
	/// </summary>
	/// <param name="argument">The argument of the tree.</param>
	public TreeSort(TArgument argument)
	{
		Argument = argument;
		HasArgument = true;
	}

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var tree = HasArgument
			? TTree.Create(Argument, source, keySelector, comparer)
			: TTree.Create(source, keySelector, comparer);

		return tree ?? Enumerable.Empty<TSource>();
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return HasArgument ? new TreeSort<T, TTree, TArgument>(Argument) : new();
	}
}

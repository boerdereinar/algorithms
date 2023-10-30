using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.SelectionSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Tournament_sort">Tournament Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class TournamentSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new TournamentSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var trees = new Stack<Tree<TSource>>(source.Select(x => new Tree<TSource>(x, keySelector(x), new())));
		while (trees.Count > 0)
		{
			var tree = PlayTournament(trees, comparer);
			yield return tree.Value;

			trees = tree.Forest;
		}
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new TournamentSort<T>();
	}

	private static Tree<TSource> PlayTournament<TSource>(Stack<Tree<TSource>> trees, IComparer<TKey> comparer)
	{
		while (trees.Count > 1)
			trees = PlayRound(trees, comparer);

		return trees.Pop();
	}

	private static Stack<Tree<TSource>> PlayRound<TSource>(Stack<Tree<TSource>> trees, IComparer<TKey> comparer)
	{
		var result = new Stack<Tree<TSource>>();
		while (trees.Count > 0)
			result.Push(trees.Count == 1 ? trees.Pop() : PlayGame(trees.Pop(), trees.Pop(), comparer));

		return result;
	}

	private static Tree<TSource> PlayGame<TSource>(Tree<TSource> tree1, Tree<TSource> tree2, IComparer<TKey> comparer)
	{
		return comparer.Compare(tree1.Key, tree2.Key) <= 0
			? Promote(tree1, tree2)
			: Promote(tree2, tree1);
	}

	private static Tree<TSource> Promote<TSource>(Tree<TSource> winner, Tree<TSource> loser)
	{
		winner.Forest.Push(loser);
		return winner;
	}

	private record Tree<TSource>(TSource Value, TKey Key, Stack<Tree<TSource>> Forest);
}

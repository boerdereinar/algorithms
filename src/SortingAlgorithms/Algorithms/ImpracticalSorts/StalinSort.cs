using SortingAlgorithms.Sorting;

namespace SortingAlgorithms.Algorithms.ImpracticalSorts;

/// <summary>
/// Represents the <a href="https://github.com/gustavo-depaula/stalin-sort">Stalin Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class StalinSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new StalinSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		using var e = source.GetEnumerator();
		var hasFirst = e.MoveNext();
		if (!hasFirst)
			yield break;

		yield return e.Current;
		var prev = keySelector(e.Current);
		while (e.MoveNext())
			if (comparer.Compare(prev, prev = keySelector(e.Current)) <= 0)
				yield return e.Current;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new StalinSort<T>();
	}
}

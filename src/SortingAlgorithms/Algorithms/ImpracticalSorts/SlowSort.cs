using SortingAlgorithms.Sorting;

namespace SortingAlgorithms.Algorithms.ImpracticalSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Slowsort">Slowsort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class SlowSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new SlowSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		Sort<TSource>(keyedArray);
		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new SlowSort<T>();
	}

	private static void Sort<TSource>(KeyedArraySegment<TSource, TKey> source)
	{
		if (source.Length <= 1)
			return;

		var m = (source.Length - 1) / 2;
		Sort(source[..(m + 1)]);
		Sort(source[(m + 1)..]);
		if (source.Compare(m, ^1) > 0)
			source.Swap(m, ^1);

		Sort(source[..^1]);
	}
}

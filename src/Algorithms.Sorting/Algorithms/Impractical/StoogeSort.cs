using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Impractical;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Stooge_sort">Stooge Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class StoogeSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new StoogeSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		if (keyedArray.Length <= 1)
			return keyedArray;

		Sort<TSource>(keyedArray);
		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new StoogeSort<T>();
	}

	private static void Sort<TSource>(KeyedArraySegment<TSource, TKey> source)
	{
		if (source.Compare(0, ^1) > 0)
			(source[0], source[^1]) = (source[^1], source[0]);

		if (source.Length <= 2)
			return;

		var t = source.Length / 3;
		Sort(source[..^t]);
		Sort(source[t..]);
		Sort(source[..^t]);
	}
}

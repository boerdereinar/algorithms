using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Exchange;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Quicksort">Quicksort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class QuickSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new QuickSort<TKey>();

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
		return new QuickSort<T>();
	}

	private static void Sort<TSource>(KeyedArraySegment<TSource, TKey> source)
	{
		if (source.Length <= 1)
			return;

		var pivot = Partition(source);
		Sort(source[..pivot]);
		Sort(source[(pivot + 1)..]);
	}

	private static int Partition<TSource>(KeyedArraySegment<TSource, TKey> source)
	{
		var pivot = source.Key(^1);
		var i = 0;
		for (var j = 0; j < source.Length - 1; j++)
			if (source.Compare(j, pivot) < 0)
				source.Swap(i++, j);

		source.Swap(i, ^1);
		return i;
	}
}

using SortingAlgorithms.Sorting;

namespace SortingAlgorithms.Algorithms.MergeSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Merge_sort">Merge Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class MergeSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new MergeSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		var keyedClone = keyedArray.Clone();

		SplitMerge<TSource>(keyedArray, keyedClone);

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new MergeSort<T>();
	}

	private static void SplitMerge<TSource>(KeyedArraySegment<TSource, TKey> a, KeyedArraySegment<TSource, TKey> b)
	{
		if (a.Length <= 1)
			return;

		var middle = a.Length / 2;
		SplitMerge(b[..middle], a[..middle]);
		SplitMerge(b[middle..], a[middle..]);

		Merge(b, a, middle);
	}

	private static void Merge<TSource>(KeyedArraySegment<TSource, TKey> a, KeyedArraySegment<TSource, TKey> b, int middle)
	{
		for (int i = 0, j = middle, k = 0; k < a.Length; k++)
		{
			if (i < middle && (j >= a.Length || a.Compare(i, j) <= 0))
				b[k] = a[i++];
			else
				b[k] = a[j++];
		}
	}
}

using Algorithms.Common.Collections;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Concurrent;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Batcher_odd%E2%80%93even_mergesort">Batcher Odd-Even Merge Sort</a>
/// sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class BatcherOddEvenMergeSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new BatcherOddEvenMergeSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);

		for (var p = 1; p < keyedArray.Length; p <<= 1)
			for (var k = p; k >= 1; k >>= 1)
				for (var j = k % p; j <= keyedArray.Length - k - 1; j += 2 * k)
					for (var i = 0; i < Math.Min(k, keyedArray.Length - j - k); i++)
						if ((i + j) / (2 * p) == (i + j + k) / (2 * p) && keyedArray.Compare(i + j, i + j + k) > 0)
							keyedArray.Swap(i + j, i + j + k);

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new BatcherOddEvenMergeSort<T>();
	}
}

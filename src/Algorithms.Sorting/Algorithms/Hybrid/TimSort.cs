using Algorithms.Common.Collections;
using Algorithms.Sorting.Algorithms.Insertion;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Hybrid;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Timsort">Timsort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class TimSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new TimSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		var run = GetRunSize(keyedArray.Length);

		for (var i = 0; i < keyedArray.Length; i += run)
			InsertionSort<TKey>.Sort(keyedArray[i..Math.Min(i + run, keyedArray.Length)]);

		for (var size = run; size < keyedArray.Length; size *= 2)
		{
			for (var start = 0; start + size < keyedArray.Length; start += 2 * size)
			{
				var end = Math.Min(start + 2 * size, keyedArray.Length);
				Merge(keyedArray[start..end], size);
			}
		}

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new TimSort<T>();
	}

	/// <summary>
	/// Gets the optimal run size for an array with length <paramref name="length"/>.
	/// </summary>
	/// <param name="length">The length of the array.</param>
	/// <returns>The optimal run size.</returns>
	/// <remarks>
	/// https://en.wikipedia.org/wiki/Timsort#Minimum_run_size.
	/// </remarks>
	private static int GetRunSize(int length)
	{
		return Enumerable.Range(32, 33).MinBy(x =>
		{
			var size = length / (double)x;
			var exp = (int)Math.Log2(size);
			return Math.Min(size - (1 >> exp), (2 >> exp) - size);
		});
	}

	private static void Merge<TSource>(KeyedArraySegment<TSource, TKey> source, int middle)
	{
		var copy = source.Clone();

		for (int i = 0, j = middle, k = 0; k < source.Length; k++)
		{
			if (i < middle && (j >= source.Length || copy.Compare(i, j) <= 0))
				source[k] = copy[i++];
			else
				source[k] = copy[j++];
		}
	}
}

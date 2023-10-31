using Algorithms.Common.Collections;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Other;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Pancake_sorting">Pancake Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class PancakeSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new PancakeSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		for (var n = keyedArray.Length; n > 1; n--)
		{
			var max = MaxIndex(keyedArray, n);
			if (max == n - 1)
				continue;

			if (max != 0)
				Flip(keyedArray, max);
			Flip(keyedArray, n - 1);
		}

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new PancakeSort<T>();
	}

	private static int MaxIndex<TSource>(KeyedArray<TSource, TKey> source, int n)
	{
		var max = 0;
		for (var i = 1; i < n; i++)
			if (source.Compare(i, max) > 0)
				max = i;

		return max;
	}

	private static void Flip<TSource>(KeyedArray<TSource, TKey> source, int n)
	{
		for (var i = 0; i < n; i++, n--)
			source.Swap(i, n);
	}
}

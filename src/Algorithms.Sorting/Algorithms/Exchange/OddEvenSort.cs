using Algorithms.Common.Collections;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Exchange;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Odd-even_sort">Odd-even Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class OddEvenSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new OddEvenSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);

		var sorted = false;
		while (!sorted)
		{
			sorted = true;
			for (var i = 1; i < keyedArray.Length - 1; i += 2)
			{
				if (keyedArray.Compare(i, i + 1) <= 0)
					continue;

				keyedArray.Swap(i, i + 1);
				sorted = false;
			}

			for (var i = 0; i < keyedArray.Length - 1; i += 2)
			{
				if (keyedArray.Compare(i, i + 1) <= 0)
					continue;

				keyedArray.Swap(i, i + 1);
				sorted = false;
			}
		}

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new OddEvenSort<T>();
	}
}

using Algorithms.Common.Collections;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Exchange;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Comb_sort">Comb Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class CombSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new CombSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		const double Shrink = 1.3;

		var keyedArray = source.ToKeyedArray(keySelector, comparer);

		var sorted = false;
		for (var gap = (int)(keyedArray.Length / Shrink); !sorted; gap = (int)(gap / Shrink))
		{
			if (gap <= 1)
			{
				gap = 1;
				sorted = true;
			}

			for (var i = 0; i + gap < keyedArray.Length; i++)
			{
				if (keyedArray.Compare(i, i + gap) <= 0)
					continue;

				keyedArray.Swap(i, i + gap);
				sorted = false;
			}
		}

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new CombSort<T>();
	}
}

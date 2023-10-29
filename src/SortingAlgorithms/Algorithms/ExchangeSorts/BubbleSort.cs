using SortingAlgorithms.Sorting;

namespace SortingAlgorithms.Algorithms.ExchangeSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Bubble_sort">Bubble Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class BubbleSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new BubbleSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		int newN;
		for (var n = keyedArray.Length; n > 1; n = newN)
		{
			newN = 0;
			for (var i = 1; i < n; i++)
			{
				if (keyedArray.Compare(i - 1, i) <= 0)
					continue;

				keyedArray.Swap(i - 1, i);
				newN = i;
			}
		}

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new BubbleSort<T>();
	}
}

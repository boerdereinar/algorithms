using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Exchange;

/// <summary>
/// Represents the <a href="https://arxiv.org/abs/1503.08498">Dual Pivot Quicksort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class DualPivotQuickSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new DualPivotQuickSort<TKey>();

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
		return new DualPivotQuickSort<T>();
	}

	private static void Sort<TSource>(KeyedArraySegment<TSource, TKey> source)
	{
		if (source.Length <= 1)
			return;

		var (l, r) = Partition(source);
		Sort(source[..l]);
		Sort(source[(l + 1)..r]);
		Sort(source[(r + 1)..]);
	}

	private static (int Left, int Right) Partition<TSource>(KeyedArraySegment<TSource, TKey> source)
	{
		if (source.Compare(0, ^1) > 0)
			source.Swap(0, ^1);

		var i = 1;
		var j = source.Length - 2;
		var p = source.Key(0);
		var q = source.Key(^1);

		for (var k = 1; k <= j; k++)
		{
			if (source.Compare(k, p) < 0)
				source.Swap(k, i++);
			else if (source.Compare(k, q) >= 0)
			{
				while (k < j && source.Compare(j, q) > 0)
					j--;

				source.Swap(k, j--);

				if (source.Compare(k, p) < 0)
					source.Swap(k, i++);
			}
		}

		source.Swap(0, --i);
		source.Swap(^1, ++j);

		return (i, j);
	}
}

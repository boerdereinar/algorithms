using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.ExchangeSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Sorting_algorithm#Exchange_sort">Exchange Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class ExchangeSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new ExchangeSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		for (var i = 0; i < keyedArray.Length - 1; i++)
			for (var j = i + 1; j < keyedArray.Length; j++)
				if (keyedArray.Compare(i, j) > 0)
					keyedArray.Swap(i, j);

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new ExchangeSort<T>();
	}
}

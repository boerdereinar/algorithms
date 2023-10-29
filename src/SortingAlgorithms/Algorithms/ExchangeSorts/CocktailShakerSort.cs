using SortingAlgorithms.Sorting;

namespace SortingAlgorithms.Algorithms.ExchangeSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Cocktail_shaker_sort">Cocktail Shaker Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class CocktailShakerSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new CocktailShakerSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		var swapped = true;
		while (swapped)
		{
			swapped = false;
			for (var i = 0; i < keyedArray.Length - 1; i++)
			{
				if (keyedArray.Compare(i, i + 1) <= 0)
					continue;

				keyedArray.Swap(i, i + 1);
				swapped = true;
			}

			if (!swapped) break;

			swapped = false;
			for (var i = keyedArray.Length - 2; i >= 0; i--)
			{
				if (keyedArray.Compare(i, i + 1) <= 0)
					continue;

				keyedArray.Swap(i, i + 1);
				swapped = true;
			}
		}

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new CocktailShakerSort<T>();
	}
}

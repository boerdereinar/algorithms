using SortingAlgorithms.Sorting;
using SortingAlgorithms.Utilities;

namespace SortingAlgorithms.Algorithms.ImpracticalSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Bogosort">Bogo Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class BogoSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new BogoSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		var random = new Random(42);

		while (!keyedArray.IsSorted())
			random.Shuffle(keyedArray);

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new BogoSort<T>();
	}
}

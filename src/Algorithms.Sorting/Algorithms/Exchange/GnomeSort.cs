using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Exchange;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Gnome_sort">Gnome Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class GnomeSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new GnomeSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);

		for (var i = 1; i < keyedArray.Length; i++)
			for (var j = i; j > 0 && keyedArray.Compare(j - 1, j) > 0; j--)
				keyedArray.Swap(j - 1, j);

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new GnomeSort<T>();
	}
}

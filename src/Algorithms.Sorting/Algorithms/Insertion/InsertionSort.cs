using Algorithms.Common.Collections;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Insertion;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Insertion_sort">Insertion Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class InsertionSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new InsertionSort<TKey>();

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
		return new InsertionSort<T>();
	}

	/// <summary>
	/// Sorts the elements of a sequence in ascending order according to a key.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	public static void Sort(Span<TKey> source, IComparer<TKey> comparer)
	{
		for (var i = 1; i < source.Length; i++)
		{
			var item = source[i];
			var j = i - 1;
			for (; j >= 0 && comparer.Compare(source[j], item) > 0; j--)
				source[j + 1] = source[j];

			source[j + 1] = item;
		}
	}

	/// <summary>
	/// Sorts the elements of a sequence in ascending order according to a key.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	internal static void Sort<TSource>(KeyedArraySegment<TSource, TKey> source)
	{
		for (var i = 1; i < source.Length; i++)
		{
			var item = source[i];
			var j = i - 1;
			for (; j >= 0 && source.Compare(j, item.Key) > 0; j--)
				source[j + 1] = source[j];

			source[j + 1] = item;
		}
	}
}

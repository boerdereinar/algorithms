using Algorithms.Common.Collections;
using Algorithms.Common.Comparers;
using Algorithms.Common.Utilities;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Hybrid;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Multi-key_quicksort">Multi-key Quicksort</a> sorting algorithm.
/// </summary>
public sealed class MultiKeyQuickSort : ISortingAlgorithm<string>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<string> Default { get; } = new MultiKeyQuickSort();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, string> keySelector, IComparer<string> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		Sort<TSource>(keyedArray, 0, CharComparer.FromStringComparer(comparer));
		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		throw new NotSupportedException();
	}

	/// <summary>
	/// Sorts the elements of a sequence in ascending order according to a key.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="d">The index to start at.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	internal static void Sort<TSource>(KeyedArraySegment<TSource, string> source, int d, IComparer<char?> comparer)
	{
		if (source.Length <= 0)
			return;

		var k = 0;
		for (var i = 0; i < source.Length; i++)
			k = Math.Max(k, source.Key(i).Length);

		Sort(source, d, k, comparer);
	}

	private static void Sort<TSource>(KeyedArraySegment<TSource, string> source, int d, int k, IComparer<char?> comparer)
	{
		if (source.Length <= 1 || d >= k)
			return;

		var (l, r) = Partition(source, d, comparer);

		Sort(source[..l], d, k, comparer);
		Sort(source[l..r], d + 1, k, comparer);
		Sort(source[r..], d, k, comparer);
	}

	private static (int Left, int Right) Partition<TSource>(KeyedArraySegment<TSource, string> source, int d, IComparer<char?> comparer)
	{
		var pivot = source.Key(0).TryGetCharAt(d);

		var l = 0;
		var r = source.Length;
		for (var i = 1; i < r;)
		{
			var c = comparer.Compare(source.Key(i).TryGetCharAt(d), pivot);
			if (c < 0)
				source.Swap(l++, i++);
			else if (c > 0)
				source.Swap(--r, i);
			else
				i++;
		}

		return (l, r);
	}
}

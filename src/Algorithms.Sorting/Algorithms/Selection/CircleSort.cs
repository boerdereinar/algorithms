using Algorithms.Common.Collections;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Selection;

/// <summary>
/// Represents the <a href="https://sourceforge.net/p/forth-4th/wiki/Circle%20sort/">Circle Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class CircleSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new CircleSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		if (keyedArray.Length <= 1)
			return keyedArray;

		while (Sort<TSource>(keyedArray)) { }

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new CircleSort<T>();
	}

	private static bool Sort<TSource>(KeyedArraySegment<TSource, TKey> source)
	{
		if (source.Length <= 1)
			return false;

		var swapped = false;
		var i = 0;
		var j = source.Length - 1;
		for (; i < j; i++, j--)
		{
			if (source.Compare(i, j) <= 0)
				continue;

			source.Swap(i, j);
			swapped = true;
		}

		if (i == j && source.Compare(i, i + 1) > 0)
		{
			source.Swap(i, i + 1);
			swapped = true;
		}

		var m = (source.Length - 1) / 2 + 1;
		swapped |= Sort(source[..m]);
		swapped |= Sort(source[m..]);

		return swapped;
	}
}

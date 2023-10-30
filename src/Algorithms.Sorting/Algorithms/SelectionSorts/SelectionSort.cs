using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.SelectionSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Selection_sort">Selection Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class SelectionSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new SelectionSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		if (keyedArray.Length == 0)
			yield break;

		for (var i = 0; i < keyedArray.Length - 1; i++)
		{
			var min = i;
			for (var j = i + 1; j < keyedArray.Length; j++)
				if (keyedArray.Compare(j, min) < 0)
					min = j;

			yield return keyedArray.Element(min);

			keyedArray.Swap(i, min);
		}

		yield return keyedArray.Element(^1);
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new SelectionSort<T>();
	}
}

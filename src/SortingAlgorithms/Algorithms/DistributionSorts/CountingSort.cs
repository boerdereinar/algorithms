using System.Numerics;
using SortingAlgorithms.Sorting;
using SortingAlgorithms.Utilities;

namespace SortingAlgorithms.Algorithms.DistributionSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Counting_sort">Counting Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class CountingSort<TKey> : ISortingAlgorithm<TKey> where TKey : IBinaryInteger<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new CountingSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		if (keyedArray.MinMax() is not var (minKey, maxKey))
			return keyedArray;

		var min = int.CreateChecked(minKey);
		var max = int.CreateChecked(maxKey);
		var range = Math.Abs(max - min) + 1;

		var count = new int[range];
		var output = new TSource[keyedArray.Length];

		foreach (var key in keyedArray.Keys)
			count[Math.Abs(int.CreateChecked(key) - min)]++;

		for (var i = 1; i < count.Length; i++)
			count[i] += count[i - 1];

		for (var i = keyedArray.Length - 1; i >= 0; i--)
		{
			var (element, key) = keyedArray[i];
			var index = Math.Abs(int.CreateChecked(key) - min);
			output[--count[index]] = element;
		}

		return output;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		throw new NotSupportedException();
	}
}

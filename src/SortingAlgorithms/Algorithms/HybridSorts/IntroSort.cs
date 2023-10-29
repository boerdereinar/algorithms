using SortingAlgorithms.Algorithms.InsertionSorts;
using SortingAlgorithms.DataStructures.Heaps;
using SortingAlgorithms.Sorting;

namespace SortingAlgorithms.Algorithms.HybridSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Introsort">Intro Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class IntroSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new IntroSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		var maxDepth = int.Log2(keyedArray.Length) << 1;
		Sort<TSource>(keyedArray, maxDepth);
		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new IntroSort<T>();
	}

	private static void Sort<TSource>(KeyedArraySegment<TSource, TKey> source, int maxDepth)
	{
		if (source.Length < 16)
			InsertionSort<TKey>.Sort(source);
		else if (maxDepth == 0)
		{
			var heapArray = source.Clone();
			var heap = new BinaryHeap<TSource, TKey>(heapArray);

			for (var i = 0; heap.TryDeleteMin(out var value, out var key); i++)
				source[i] = (value, key);
		}
		else
		{
			var p = Partition(source);
			Sort(source[..p], maxDepth - 1);
			Sort(source[(p + 1)..], maxDepth - 1);
		}
	}

	private static int Partition<TSource>(KeyedArraySegment<TSource, TKey> source)
	{
		var pivot = source.Key(^1);
		var i = 0;
		for (var j = 0; j < source.Length - 1; j++)
			if (source.Compare(j, pivot) < 0)
				source.Swap(i++, j);

		source.Swap(i, ^1);
		return i;
	}
}

using Algorithms.DataStructures.Heaps;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Selection;

/// <summary>
/// Represents a generic <a href="https://en.wikipedia.org/wiki/Heapsort">Heapsort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
/// <typeparam name="THeap">The type of the heap.</typeparam>
public class HeapSort<TKey, THeap> : ISortingAlgorithm<TKey> where THeap : IHeap
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new HeapSort<TKey, THeap>();

	/// <inheritdoc />
	public virtual IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var heap = THeap.Create(source, keySelector, comparer);
		while (heap.TryDeleteMin(out var value, out _))
			yield return value;
	}

	/// <inheritdoc />
	public virtual ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new HeapSort<T, THeap>();
	}
}

using Algorithms.DataStructures.Heaps;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Selection;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Weak_heap#Weak-heap_sort">Weak-Heap Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class WeakHeapSort<TKey> : HeapSort<TKey, WeakHeap>
{
	/// <inheritdoc cref="HeapSort{TKey,THeap}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new WeakHeapSort<TKey>();
}

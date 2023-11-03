using Algorithms.DataStructures.Heaps;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Selection;

/// <summary>
/// Represents the heapsort algorithm implemented in <see cref="PriorityQueue{TElement,TPriority}"/>.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class DefaultHeapSort<TKey> : HeapSort<TKey, DefaultHeap>
{
	/// <inheritdoc cref="HeapSort{TKey,THeap}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new DefaultHeapSort<TKey>();
}

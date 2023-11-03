using Algorithms.DataStructures.Heaps;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Selection;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Leftist_tree">Leftist Heapsort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class LeftistHeapSort<TKey> : HeapSort<TKey, LeftistHeap>
{
	/// <inheritdoc cref="HeapSort{TKey,THeap}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new LeftistHeapSort<TKey>();
}

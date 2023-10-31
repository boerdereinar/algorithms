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
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var heap = THeap.Create(source, keySelector, comparer);
		while (heap.TryDeleteMin(out var value, out _))
			yield return value;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new HeapSort<T, THeap>();
	}
}

/// <summary>
/// Represents the heapsort algorithm implemented in <see cref="PriorityQueue{TElement,TPriority}"/>.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class DefaultHeapSort<TKey> : HeapSort<TKey, DefaultHeap>
{
	/// <inheritdoc cref="HeapSort{TKey,THeap}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new DefaultHeapSort<TKey>();
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Heap_sort">Binary Heapsort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class BinaryHeapSort<TKey> : HeapSort<TKey, BinaryHeap>
{
	/// <inheritdoc cref="HeapSort{TKey,THeap}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new BinaryHeapSort<TKey>();
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Binomial_heap">Binomial Heapsort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class BinomialHeapSort<TKey> : HeapSort<TKey, BinomialHeap>
{
	/// <inheritdoc cref="HeapSort{TKey,THeap}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new BinomialHeapSort<TKey>();
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Leftist_tree">Leftist Heapsort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class LeftistHeapSort<TKey> : HeapSort<TKey, LeftistHeap>
{
	/// <inheritdoc cref="HeapSort{TKey,THeap}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new LeftistHeapSort<TKey>();
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Weak_heap#Weak-heap_sort">Weak-Heap Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class WeakHeapSort<TKey> : HeapSort<TKey, WeakHeap>
{
	/// <inheritdoc cref="HeapSort{TKey,THeap}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new WeakHeapSort<TKey>();
}

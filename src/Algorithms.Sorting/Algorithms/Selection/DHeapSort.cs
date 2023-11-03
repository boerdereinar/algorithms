using Algorithms.DataStructures.Heaps;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Selection;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/D-ary_heap">D-Heap Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class DHeapSort<TKey> : HeapSort<TKey, DHeap>
{
	private readonly int _d;

	/// <summary>
	/// Initializes a new instance of the <see cref="DHeapSort{TKey}"/> class.
	/// </summary>
	public DHeapSort() : this(2) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="DHeapSort{TKey}"/> class.
	/// </summary>
	/// <param name="d">The number of children in each node.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="d"/> is less than 2.</exception>
	public DHeapSort(int d)
	{
		if (d < 2)
			throw new ArgumentOutOfRangeException(nameof(d), d, "The number of children in each node must be at least 2.");

		_d = d;
	}

	/// <inheritdoc cref="HeapSort{TKey,THeap}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new DHeapSort<TKey>();

	/// <inheritdoc />
	public override IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var heap = DHeap.Create(_d, source, keySelector, comparer);
		while (heap.TryDeleteMin(out var value, out _))
			yield return value;
	}

	/// <inheritdoc />
	public override ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new DHeapSort<T>(_d);
	}
}
